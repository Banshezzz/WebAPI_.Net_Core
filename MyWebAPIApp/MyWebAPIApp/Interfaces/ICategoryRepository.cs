using MyWebAPIApp.Models;

namespace MyWebAPIApp.Interfaces
{
    public interface ICategoryRepository
    { 
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        Category GetCategory(string name);
        ICollection<Pokemon> GetPokemonByCategory(int id);
        bool CategoryExists(int id);
    }
}
