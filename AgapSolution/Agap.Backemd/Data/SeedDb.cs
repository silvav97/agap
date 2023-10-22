using Agap.Backemd.Helpers;
using Agap.Backemd.Services;
using Agap.Shared.Entities;
using Agap.Shared.Enums;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Agap.Backemd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckFertilizersAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Andres", "Vasquez", "avasquez@yopmail.com", "314 311 4450", "Hollywood", "user.jpg", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                string filePath;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                }
                else
                {
                    filePath = $"{Environment.CurrentDirectory}/Images/users/{image}";
                }

                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                    Photo = imagePath,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckFertilizersAsync()
        {
            if (!_context.Fertilizers.Any())
            {
                _context.Fertilizers.Add(new Fertilizer { Name = "Tierra De Diatomeas Diatomita", Brand = "Diatomeas Colombia", PricePerGram = 3.60F });
                _context.Fertilizers.Add(new Fertilizer { Name = "ABONO Y FERTILIZANTE PLATANERO 10-4-14", Brand = "Agro tropico", PricePerGram = 3.23F });
                _context.Fertilizers.Add(new Fertilizer { Name = "MICORRIZAS HONGOS DEL SUELO ", Brand = "Campor Verde", PricePerGram = 1.25F });
                _context.Fertilizers.Add(new Fertilizer { Name = "POTASIO FERTILIZANTE PRODUPLANT", Brand = "Campofert", PricePerGram = 18.58F });
                _context.Fertilizers.Add(new Fertilizer { Name = "FERTILIZANTE EDÁFICO FOSFITEK POTASIO GOLD", Brand = "Efitec", PricePerGram = 23.51F });
                _context.Fertilizers.Add(new Fertilizer { Name = "LEONARDITA, ACONDICIONADOR DE SUELOS", Brand = "Diatomeas Colombia", PricePerGram = 2.40F });
                _context.Fertilizers.Add(new Fertilizer { Name = "BEAUVERIA BASSIANA", Brand = "Copralab", PricePerGram = 140.00F });
                _context.Fertilizers.Add(new Fertilizer { Name = "CACAO CRECIMIENTO", Brand = "BioPotent", PricePerGram = 2.12F });
                _context.Fertilizers.Add(new Fertilizer { Name = "Acondicionador Magnesio Sucre 30", Brand = "Minerargro", PricePerGram = 0.71F });
                _context.Fertilizers.Add(new Fertilizer { Name = "FERTILIZANTE 25-4-24", Brand = "Calferquim", PricePerGram = 2.55F });
                _context.Fertilizers.Add(new Fertilizer { Name = "AGRIMINS TOTTAL INICIO 11-22-5", Brand = "Colinagro", PricePerGram = 4.30F });

                /* _context.Countries.Add(new Country { Name = "Colombia" });
                 await _context.SaveChangesAsync();

                 var countryId = _context.Countries.Single(c => c.Name == "Colombia").Id; // Obtener el Id auto-generado
                 _context.States.Add(new State { Name = "Antioquia", CountryId = countryId });
                 await _context.SaveChangesAsync();

                 var stateId = _context.States.Single(s => s.Name == "Antioquia").Id;
                 _context.Cities.Add(new City { Name = "Medellín", StateId = stateId });
                */
                await _context.SaveChangesAsync(); 
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (_context.Countries.Any())
            {
                var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
                if (responseCountries.WasSuccess)
                {
                    var countries = responseCountries.Result!;
                    foreach (var countryResponse in countries)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.WasSuccess)
                            {
                                var states = responseStates.Result!;
                                foreach (var stateResponse in states!)
                                {
                                    var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.WasSuccess)
                                        {
                                            var cities = responseCities.Result!;
                                            foreach (var cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}