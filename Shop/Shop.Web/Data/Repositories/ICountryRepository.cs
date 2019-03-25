 

namespace Shop.Web.Data
{
    using Entities;
    using System.Linq;
    using System.Threading.Tasks;  
    using Models;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountriesWithCities();

        Task<Country> GetCountryWithCitiesAsync(int id);

        Task<City> GetCityAsync(int id);

        Task AddCityAsync(CityViewModel model);

        Task<int> UpdateCityAsync(City city);

        Task<int> DeleteCityAsync(City city);

        IEnumerable<SelectListItem> GetComboCountries();

        IEnumerable<SelectListItem> GetComboCities(int conuntryId);

        Task<Country> GetCountryAsync(City city);

    }

}
