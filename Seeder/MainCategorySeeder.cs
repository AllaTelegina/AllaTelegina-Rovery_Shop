using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Seeder
{
    public class MainCategorySeeder
    {
        public readonly AplicationContext _contextMainCategory;
        public MainCategorySeeder(AplicationContext context)
        {
            _contextMainCategory = context;
        }
        public async Task SeedAsync()
        {
            if (!_contextMainCategory.MainCategoryes.Any())
            {
                var mainCategoryes = new List<MainCategory>
                {
                    new MainCategory{Name="Велосипеды"},
                    new MainCategory{Name="Электровелосипеды"},
                    new MainCategory{Name="Самокаты"},
                    new MainCategory{Name="Электросамокаты"},
                    new MainCategory{Name="Аксессуары"}
                };
                await _contextMainCategory.MainCategoryes.AddRangeAsync(mainCategoryes);
                await _contextMainCategory.SaveChangesAsync();
            }
        }
    }
}
