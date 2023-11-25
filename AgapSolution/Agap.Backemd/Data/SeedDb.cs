using Agap.Backemd.Helpers;
using Agap.Backemd.Services;
using Agap.Shared.Entities;
using Agap.Shared.Entities.Agap.Shared.Entities;
using Agap.Shared.Enums;
using Agap.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;

namespace Agap.Backemd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;
        private readonly IRuntimeInformationWrapper _runtimeInformationWrapper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage, IRuntimeInformationWrapper runtimeInformationWrapper)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
            _runtimeInformationWrapper = runtimeInformationWrapper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync2();
            await CheckFertilizersAsync();
            await CheckPesticidesAsync();
            await CheckCropTypesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Andres", "Vasquez", "avasquez@yopmail.com", "314 311 4450", "Hollywood", "user.jpg", UserType.Admin);
            await CheckUserAsync("1011", "Veronica", "Montoya", "vero@yopmail.com", "313 311 4450", "Medellin", "user.jpg", UserType.Farmer);
            await CheckProjectsAsync();
            await CheckCropsAsync();
            await CheckExpensesAsync();
            await CheckProjectReportsAsync();
            await CheckCropReportsAsync();
            await CheckNotificationsAsync();
        }

        private async Task CheckCountriesAsync2()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Antioquia",
                            Cities = new List<City>
                            {
                                new City
                                {
                                    Name = "Medellín"
                                }
                            }
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
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
                if (_runtimeInformationWrapper.IsOSPlatform(OSPlatform.Windows))
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
            await _userHelper.CheckRoleAsync(UserType.Farmer.ToString());
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

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCropTypesAsync()
        {
            if (!_context.CropTypes.Any())
            {
                var cropTypeNames = new List<string>
        {
            "Banano",
            "Mango",
            "Guayaba",
            "Ruby Corn",
            "Piña",
            "Carambolo",
            "Coco",
            "Papaya",
            "Pitaya",
            "Liches"
        };
                var fertilizer = _context.Fertilizers.Single(f => f.Name == "CACAO CRECIMIENTO");
                var pesticide = _context.Pesticides.Single(p => p.Name == "Propanil");

                foreach (var name in cropTypeNames)
                {
                    _context.CropTypes.Add(new CropType
                    {
                        Weather = "Caliente",
                        Name = name,
                        PlantQuantityPerSquareMeter = new Random().Next(1, 100),
                        HarvestTime = new Random().Next(1, 100),
                        FertilizerId = fertilizer.Id,
                        FertilizerQuantityPerPlant = new Random().Next(1, 10),
                        FertilizerFrequency = new Random().Next(1, 100),
                        PesticideId = pesticide.Id,
                        PesticideQuantityPerPlant = new Random().Next(1, 10),
                        PesticideFrequency = new Random().Next(1, 100),
                        Fertilizer = fertilizer,
                        Pesticide = pesticide
                    });
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPesticidesAsync()
        {
            if (!_context.Pesticides.Any())
            {
                _context.Pesticides.Add(new Pesticide { Name = "2,4-D Ácido", Brand = "Diatomeas Colombia", PricePerGram = 3.60F });
                _context.Pesticides.Add(new Pesticide { Name = "2,4-D Amina", Brand = "Agro tropico", PricePerGram = 3.23F });
                _context.Pesticides.Add(new Pesticide { Name = "2,4-D Ester Isobutilico", Brand = "Campor Verde", PricePerGram = 1.25F });
                _context.Pesticides.Add(new Pesticide { Name = "Atrazina", Brand = "Campofert", PricePerGram = 18.58F });
                _context.Pesticides.Add(new Pesticide { Name = "Clorpirifos", Brand = "Efitec", PricePerGram = 23.51F });
                _context.Pesticides.Add(new Pesticide { Name = "Glifosato", Brand = "Diatomeas Colombia", PricePerGram = 2.40F });
                _context.Pesticides.Add(new Pesticide { Name = "Metamidofos", Brand = "Copralab", PricePerGram = 140.00F });
                _context.Pesticides.Add(new Pesticide { Name = "Metil paratión", Brand = "BioPotent", PricePerGram = 2.12F });
                _context.Pesticides.Add(new Pesticide { Name = "Paraquat", Brand = "Minerargro", PricePerGram = 0.71F });
                _context.Pesticides.Add(new Pesticide { Name = "Picloram", Brand = "Calferquim", PricePerGram = 2.55F });
                _context.Pesticides.Add(new Pesticide { Name = "Propanil", Brand = "Colinagro", PricePerGram = 4.30F });
                 
                await _context.SaveChangesAsync(); 
            }
        }

        private async Task CheckProjectsAsync()
        {
            if (!_context.Projects.Any())
            {
                var cropTypeBanano = _context.CropTypes.Single(cropType => cropType.Name == "Banano");
                var cropTypeMango = _context.CropTypes.Single(cropType => cropType.Name == "Mango");
                var cropTypeGuayaba = _context.CropTypes.Single(cropType => cropType.Name == "Guayaba");
                var cropTypeCoco = _context.CropTypes.Single(cropType => cropType.Name == "Coco");

                _context.Projects.Add(new Project { Name = "Proyecto Banano", CropTypeId = cropTypeBanano.Id, CropType = cropTypeBanano, Status = ProjectStatus.Creado, StartDate = DateTime.Now, Municipality = "Medellin", TotalBudget = 1.435F });
                _context.Projects.Add(new Project { Name = "Proyecto Mango", CropTypeId = cropTypeMango.Id, CropType = cropTypeMango, Status = ProjectStatus.Cerrado, StartDate = DateTime.Now, Municipality = "Puerto Berrio", TotalBudget = 1.435F });
                _context.Projects.Add(new Project { Name = "Proyecto Guayaba", CropTypeId = cropTypeGuayaba.Id, CropType = cropTypeGuayaba, Status = ProjectStatus.Creado, StartDate = DateTime.Now, Municipality = "Segovia", TotalBudget = 1.435F });
                _context.Projects.Add(new Project { Name = "Proyecto Coco", CropTypeId = cropTypeCoco.Id, CropType = cropTypeCoco, Status = ProjectStatus.Cerrado, StartDate = DateTime.Now, Municipality = "Copacabana", EndDate = DateTime.Now, TotalBudget = 1.435F });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCropsAsync()
        {
            if (!_context.Crops.Any())
            {
                var user = _context.Users.Single(user => user.Email == "avasquez@yopmail.com");
                var project1 = _context.Projects.Single(project => project.Name == "Proyecto Banano");
                var project2 = _context.Projects.Single(project => project.Name == "Proyecto Mango");

                _context.Crops.Add(new Crop { UserId = user.Id, User = user, ProjectId = project1.Id, Project = project1, Name = "Finca productora norte", Status = CropStatus.Creado, StartDate = DateTime.Now, ExpectedExpense = 14.23F, AssignedBudget = 34.341F, SaleValue = 23.98F, Area = 2 });
                _context.Crops.Add(new Crop { UserId = user.Id, User = user, ProjectId = project1.Id, Project = project1, Name = "Finca productora sur", Status = CropStatus.Creado, StartDate = DateTime.Now, ExpectedExpense = 14.23F, AssignedBudget = 34.341F, SaleValue = 23.98F, Area = 2 });
                _context.Crops.Add(new Crop { UserId = user.Id, User = user, ProjectId = project2.Id, Project = project2, Name = "Finca productora oriente", Status = CropStatus.Creado, StartDate = DateTime.Now, ExpectedExpense = 14.23F, AssignedBudget = 34.341F, SaleValue = 23.98F, Area = 2 });
                _context.Crops.Add(new Crop { UserId = user.Id, User = user, ProjectId = project2.Id, Project = project2, Name = "Finca productora occidente", Status = CropStatus.Creado, StartDate = DateTime.Now, ExpectedExpense = 14.23F, AssignedBudget = 34.341F, SaleValue = 23.98F, Area = 2 });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckExpensesAsync()
        {
            if (!_context.Expenses.Any())
            {
                var crop = _context.Crops.FirstOrDefault(crop => crop.Name == "Finca productora norte");

                _context.Expenses.Add(new Expense { CropId = crop.Id, Crop = crop, ExpenseValue = 1.5F, ExpenseDescription = ExpenseType.Pesticida, ExpenseDate = DateTime.Now });
                _context.Expenses.Add(new Expense { CropId = crop.Id, Crop = crop, ExpenseValue = 1.2F, ExpenseDescription = ExpenseType.Fertilizante, ExpenseDate = DateTime.Now });
                _context.Expenses.Add(new Expense { CropId = crop.Id, Crop = crop, ExpenseValue = 1.8F, ExpenseDescription = ExpenseType.Trabajadores, ExpenseDate = DateTime.Now });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProjectReportsAsync()
        {
            if (!_context.ProjectReports.Any())
            {
                var projectBanano = _context.Projects.Single(project => project.Name == "Proyecto Banano");
                var projectMango = _context.Projects.Single(project => project.Name == "Proyecto Mango");


                _context.ProjectReports.Add(new ProjectReport { Project = projectBanano, ProjectId = projectBanano.Id, ExpectedExpense = 12.5F, RealExpense = 13.4F, Profit = 30F, TotalSale = 29F, TotalBudget = 21.2F, Profitability = 70F });
                _context.ProjectReports.Add(new ProjectReport { Project = projectMango, ProjectId = projectMango.Id, ExpectedExpense = 11.6F, RealExpense = 21.4F, Profit = 45.2F, TotalSale = 39F, TotalBudget = 21.2F, Profitability = 80.4F });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCropReportsAsync()
        {
            if (!_context.CropReports.Any())
            {
                var cropFincaFeliz = _context.Crops.Single(crop => crop.Name == "Finca productora norte");
                var cropFincaTriste = _context.Crops.Single(crop => crop.Name == "Finca productora sur");


                _context.CropReports.Add(new CropReport { Crop = cropFincaFeliz, CropId = cropFincaFeliz.Id, ExpectedExpense = 12.5F, RealExpense = 13.4F, Profit = 30F, TotalSale = 29F, AssignedBudget = 21.2F, Profitability = 70F });
                _context.CropReports.Add(new CropReport { Crop = cropFincaTriste, CropId = cropFincaTriste.Id, ExpectedExpense = 11.6F, RealExpense = 21.4F, Profit = 45.2F, TotalSale = 39F, AssignedBudget = 21.2F, Profitability = 80.4F });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckNotificationsAsync()
        {
            if (!_context.Notifications.Any())
            {
                var crops = await _context.Crops.ToListAsync();
                var random = new Random();

                foreach (var crop in crops)
                {
                    _context.Notifications.Add(new Notification
                    {
                        CropId = crop.Id,
                        TitleMessage = "Recordatorio de Tarea",
                        BodyMessage = "Es hora de aplicar el tratamiento para el cultivo.",
                        Periodicity = random.Next(1, 31)
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
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