using BLL.Services;
using Core.Entities;
using Core.Enums;
using Core.Static;
using DAL;
using DAL.Repositories;
using Microsoft.CodeAnalysis.Scripting;

namespace HomeSeeker
{
    public class SeedManager
    {
        public static async Task Seed(IServiceProvider services)
        {
            //await SeedAdminUser(services);
            //await SeedCategories(services);
            //await SeedUsersAndProfiles(services);
            //await SeedAnnouncements(services);
        }

        private static async Task SeedAdminUser(IServiceProvider services)
        {
            /*var repos = services.GetRequiredService<IUserRepository>();
            var userService = services.GetRequiredService<IUserService>();

            var adminUser = await repos.GetByLoginAsync("admin");

            if (adminUser is null)
            {
                adminUser = new User
                {
                    Login = "admin",
                    Password = "admin",
                    Name = "MainAdmin",
                    Gender = 1,
                    Admin = true
                };
                await userService.Create(adminUser, "SeedManager");
            }*/
        }

        private static async Task SeedCategories(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HomeSeekerDbContext>();

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "Квартира", Description = "Жильё в многоквартирном доме" },
                new Category { Name = "Дом", Description = "Отдельно стоящий жилой дом" },
                new Category { Name = "Комната", Description = "Отдельная комната в квартире или общежитии" },
                new Category { Name = "Земельный участок", Description = "Участок под застройку или сад" }
            };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedUsersAndProfiles(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HomeSeekerDbContext>();

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Email = "admin@example.com",
                        PasswordHash = HashPasswordHelper.HashPassword("admin123"),
                        Profile = new Profile
                        {
                            FirstName = "Admin",
                            LastName = "User",
                            Role = RoleEnum.Admin,
                            PhoneNumber = "+1234567890"
                        }
                    },
                    new User
                    {
                        Email = "user1@example.com",
                        PasswordHash = HashPasswordHelper.HashPassword("password1"),
                        Profile = new Profile
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Role = RoleEnum.RegisteredUser,
                            PhoneNumber = "+1111111111"
                        }
                    },
                    new User
                    {
                        Email = "user2@example.com",
                        PasswordHash = HashPasswordHelper.HashPassword("password2"),
                        Profile = new Profile
                        {
                            FirstName = "Jane",
                            LastName = "Smith",
                            Role = RoleEnum.RegisteredUser,
                            PhoneNumber = "+2222222222"
                        }
                    },
                    new User
                    {
                        Email = "guest@example.com",
                        PasswordHash = HashPasswordHelper.HashPassword("guest123"),
                        Profile = new Profile
                        {
                            FirstName = "Guest",
                            LastName = "User",
                            Role = RoleEnum.RegisteredUser,
                            PhoneNumber = "+3333333333"
                        }
                    }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedAnnouncements(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HomeSeekerDbContext>();

            if (context.Announcements.Any())
                return;

            var profileIds = context.Profiles.Select(p => p.Id).ToList();
            var categoryIds = context.Categories.Select(c => c.Id).ToList();

            if (!profileIds.Any() || !categoryIds.Any())
                return;

            var announcements = new List<Announcement>
            {
                new Announcement
                {
                    Title = "Затишна студія в центрі Києва",
                    PropertyType = "Apartment",
                    PublishedAt = DateTime.UtcNow.AddDays(-3),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[0],
                    CategoryID = 4,
                    Description = new ObjectDescription
                    {
                        Price = 5800000,
                        Details = "Світла студія біля метро, магазинів та парку Шевченка.",
                        SquareMeters = 32,
                        Address = "Київ, вул. Хрещатик, 12",
                        Rooms = 1,
                        Latitude = 50.4489,
                        Longitude = 30.5221
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Двокімнатна квартира біля парку",
                    PropertyType = "Apartment",
                    PublishedAt = DateTime.UtcNow.AddDays(-7),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[1 % profileIds.Count],
                    CategoryID = 4,
                    Description = new ObjectDescription
                    {
                        Price = 7800000,
                        Details = "Поруч школа, дитячий садок, зелений район.",
                        SquareMeters = 54,
                        Address = "Львів, просп. Свободи, 45",
                        Rooms = 2,
                        Latitude = 49.8429,
                        Longitude = 24.0315
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Будинок з ділянкою 10 соток",
                    PropertyType = "House",
                    PublishedAt = DateTime.UtcNow.AddDays(-14),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[2 % profileIds.Count],
                    CategoryID = 5,
                    Description = new ObjectDescription
                    {
                        Price = 12000000,
                        Details = "Просторий будинок з садом, гаражем та альтанкою.",
                        SquareMeters = 120,
                        Address = "Київська область, с. Зелена, вул. Сонячна, 1",
                        Rooms = 4,
                        Latitude = 50.4017,
                        Longitude = 30.2528
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Апартаменти бізнес-класу",
                    PropertyType = "Apartment",
                    PublishedAt = DateTime.UtcNow.AddDays(-5),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[3 % profileIds.Count],
                    CategoryID = 4,
                    Description = new ObjectDescription
                    {
                        Price = 10500000,
                        Details = "Житловий комплекс з охороною, паркінгом, фітнесом та рестораном.",
                        SquareMeters = 60,
                        Address = "Київ, вул. Нова, 25",
                        Rooms = 2,
                        Latitude = 50.4501,
                        Longitude = 30.5234
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Простора трикімнатна квартира біля метро",
                    PropertyType = "Apartment",
                    PublishedAt = DateTime.UtcNow.AddDays(-2),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[4 % profileIds.Count],
                    CategoryID = 4,
                    Description = new ObjectDescription
                    {
                        Price = 8900000,
                        Details = "Балкон, велика кухня, вбудована техніка, шумоізоляція.",
                        SquareMeters = 74,
                        Address = "Харків, вул. Перемоги, 8",
                        Rooms = 3,
                        Latitude = 49.9935,
                        Longitude = 36.2304
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Котедж у сосновому лісі",
                    PropertyType = "House",
                    PublishedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[5 % profileIds.Count],
                    CategoryID = 5,
                    Description = new ObjectDescription
                    {
                        Price = 15000000,
                        Details = "Закрита територія, камін, сауна, свіже повітря.",
                        SquareMeters = 200,
                        Address = "Черкаська область, с. Сосновий Бір, вул. Лісова, 9",
                        Rooms = 5,
                        Latitude = 49.4285,
                        Longitude = 32.0621
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Невелика дача на літо",
                    PropertyType = "Dacha",
                    PublishedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[6 % profileIds.Count],
                    CategoryID = 5,
                    Description = new ObjectDescription
                    {
                        Price = 2400000,
                        Details = "6 соток, фруктові дерева, літній душ і кухня.",
                        SquareMeters = 40,
                        Address = "Київська область, СТ Весна, вул. Яблунева, 4",
                        Rooms = 2,
                        Latitude = 50.5123,
                        Longitude = 30.3050
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                },
                new Announcement
                {
                    Title = "Елітний пентхаус з видом на Дніпро",
                    PropertyType = "Penthouse",
                    PublishedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Active",
                    ProfileID = profileIds[7 % profileIds.Count],
                    CategoryID = 4,
                    Description = new ObjectDescription
                    {
                        Price = 28000000,
                        Details = "Панорамні вікна, тераса, ліфт прямо в квартиру.",
                        SquareMeters = 150,
                        Address = "Київ, Набережно-Хрещатицька, 2",
                        Rooms = 4,
                        Latitude = 50.4565,
                        Longitude = 30.5229
                    },
                    Images = new List<Image> {
                        new Image { Url = "/images/logo.png", IsMain = true }
                    }
                }
            };

            await context.Announcements.AddRangeAsync(announcements);
            await context.SaveChangesAsync();
        }

    }
}
