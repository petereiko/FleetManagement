using FleetManagement.UI.Models.CompanyAssetDto;
using FleetManagement.UI.Models.DriverDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class BaseController : Controller
    {
        protected List<Driver> GetDriversFromSession()
        {
            var json = HttpContext.Session.GetString("Drivers");
            if (string.IsNullOrEmpty(json))
            {
                var drivers = new List<Driver>
                {
                    new Driver {
                Id = "D1",
                FirstName = "Eddie",
                MiddleName = "Sunday",
                LastName = "Hoyte",
                Email = "eddie@example.com",
                PhoneNumber = "08012345678",
                Address = "123 Main St",
                DateOfBirth = new DateTime(1985, 5, 15),
                Gender = "Male",
                LicenseNumber = "LIC12345",
                LicenseExpiryDate = DateTime.Now.AddYears(2),
                LicenseCategory = "Class A",
                DateHired = DateTime.Now.AddYears(-3),
                EmploymentStatus = "Active",
                EmergencyContactName = "John Hoyte",
                EmergencyContactPhone = "08087654321",
                Relationship = "Brother"
            },
            new Driver {
                Id = "D2",
                FirstName = "Jane",
                MiddleName = "Jessica",
                LastName = "Smith",
                Email = "jane@example.com",
                PhoneNumber = "08023456789",
                Address = "456 Market Ave",
                DateOfBirth = new DateTime(1990, 8, 22),
                Gender = "Female",
                LicenseNumber = "LIC67890",
                LicenseExpiryDate = DateTime.Now.AddYears(1),
                LicenseCategory = "Class B",
                DateHired = DateTime.Now.AddYears(-1),
                EmploymentStatus = "Active",
                EmergencyContactName = "Mary Smith",
                EmergencyContactPhone = "08098765432",
                Relationship = "Mother"
            },
            new Driver {
                Id = "D3",
                FirstName = "Gbenga",
                MiddleName = "Samuel",
                LastName = "Tokunbo",
                Email = "gbenga@example.com",
                PhoneNumber = "08034567890",
                Address = "789 Broad St",
                DateOfBirth = new DateTime(1988, 12, 5),
                Gender = "Male",
                LicenseNumber = "LIC54321",
                LicenseExpiryDate = DateTime.Now.AddYears(3),
                LicenseCategory = "Class A",
                DateHired = DateTime.Now.AddYears(-2),
                EmploymentStatus = "Active",
                EmergencyContactName = "Tunde Tokunbo",
                EmergencyContactPhone = "08065432109",
                Relationship = "Cousin"
            },
            new Driver {
    Id = "D4",
    FirstName = "Adebayo",
    MiddleName = "Michael",
    LastName = "Olawale",
    Email = "adebayo@example.com",
    PhoneNumber = "08045678901",
    Address = "15 Unity Close, Ikeja",
    DateOfBirth = new DateTime(1992, 4, 10),
    Gender = "Male",
    LicenseNumber = "LIC98765",
    LicenseExpiryDate = DateTime.Now.AddYears(2),
    LicenseCategory = "Class B",
    DateHired = DateTime.Now.AddYears(-4),
    EmploymentStatus = "Active",
    EmergencyContactName = "Yemi Olawale",
    EmergencyContactPhone = "08067891234",
    Relationship = "Brother"
},
            new Driver {
                Id = "D5",
                FirstName = "Fatima",
                MiddleName = "Aisha",
                LastName = "Bello",
                Email = "fatima@example.com",
                PhoneNumber = "08056789012",
                Address = "22 Main Street, Kaduna",
                DateOfBirth = new DateTime(1995, 7, 18),
                Gender = "Female",
                LicenseNumber = "LIC65432",
                LicenseExpiryDate = DateTime.Now.AddYears(3),
                LicenseCategory = "Class C",
                DateHired = DateTime.Now.AddYears(-2),
                EmploymentStatus = "Active",
                EmergencyContactName = "Musa Bello",
                EmergencyContactPhone = "08078901234",
                Relationship = "Father"
            },
            new Driver {
                Id = "D6",
                FirstName = "Emeka",
                MiddleName = "John",
                LastName = "Okonkwo",
                Email = "emeka@example.com",
                PhoneNumber = "08067890123",
                Address = "10 Park Avenue, Enugu",
                DateOfBirth = new DateTime(1987, 10, 25),
                Gender = "Male",
                LicenseNumber = "LIC32145",
                LicenseExpiryDate = DateTime.Now.AddYears(1),
                LicenseCategory = "Class A",
                DateHired = DateTime.Now.AddYears(-5),
                EmploymentStatus = "Active",
                EmergencyContactName = "Chinwe Okonkwo",
                EmergencyContactPhone = "08089012345",
                Relationship = "Wife"
            }
                    // Add additional drivers as needed...
                };
                HttpContext.Session.SetString("Drivers", JsonSerializer.Serialize(drivers));
                return drivers;
            }
            return JsonSerializer.Deserialize<List<Driver>>(json);
        }

        protected List<Vehicle> GetVehiclesFromSession()
        {
            var json = HttpContext.Session.GetString("Vehicles");
            if (string.IsNullOrEmpty(json))
            {
                var vehicles = new List<Vehicle>
                {
                    new Vehicle {
                        Id = "V1", Make = "Toyota", Model = "Camry", Year = 2020, VIN = "VIN001", LicensePlate = "ABC123", Color = "White",
                        EngineNumber = "ENG001", ChassisNumber = "CHS001", RegistrationDate = DateTime.Now.AddYears(-1),
                        LastServiceDate = DateTime.Now.AddMonths(-3), Mileage = 15000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(6),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D1", AssignedDriverName = "Eddie Hoyte"
                    },
                    new Vehicle {
                        Id = "V2", Make = "Honda", Model = "Civic", Year = 2019, VIN = "VIN002", LicensePlate = "XYZ789", Color = "Black",
                        EngineNumber = "ENG002", ChassisNumber = "CHS002", RegistrationDate = DateTime.Now.AddYears(-2),
                        LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 20000, FuelType = "Petrol", Transmission = "Manual",
                        InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(8),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(5), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D2", AssignedDriverName = "Jane Smith"
                    },
                    new Vehicle {
                        Id = "V3", Make = "Ford", Model = "Focus", Year = 2021, VIN = "VIN003", LicensePlate = "FOC456", Color = "Blue",
                        EngineNumber = "ENG003", ChassisNumber = "CHS003", RegistrationDate = DateTime.Now.AddMonths(-10),
                        LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 10000, FuelType = "Diesel", Transmission = "Automatic",
                        InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(10),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D3", AssignedDriverName = "Gbenga Tokunbo"
                    },
                    new Vehicle { Id = "V4", Make = "Chevrolet", Model = "Malibu", Year = 2018, VIN = "VIN004", LicensePlate = "MAL321", Color = "Silver", EngineNumber = "ENG004", ChassisNumber = "CHS004", RegistrationDate = DateTime.Now.AddYears(-3), LastServiceDate = DateTime.Now.AddMonths(-4), Mileage = 30000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(5), RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = false },
                    new Vehicle { Id = "V5", Make = "Nissan", Model = "Altima", Year = 2022, VIN = "VIN005", LicensePlate = "ALT654", Color = "Red", EngineNumber = "ENG005", ChassisNumber = "CHS005", RegistrationDate = DateTime.Now.AddMonths(-2), LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 5000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(12), RoadWorthyExpiryDate = DateTime.Now.AddMonths(7), Status = "Active", IsAssigned = false },
                    new Vehicle { Id = "V6", Make = "Mercedes Benz", Model = "E360", Year = 2022, VIN = "VIN006", LicensePlate = "GBV856", Color = "Gray", EngineNumber = "ENG006", ChassisNumber = "CHS006", RegistrationDate = DateTime.Now.AddMonths(-3), LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 8000, FuelType = "Diesel", Transmission = "Automatic", InsuranceCompany = "PremiumInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(9), RoadWorthyExpiryDate = DateTime.Now.AddMonths(8), Status = "Active", IsAssigned = false },
                    new Vehicle { Id = "V7", Make = "Toyota", Model = "Tundra", Year = 2018, VIN = "VIN007", LicensePlate = "VALL423", Color = "Blue", EngineNumber = "ENG007", ChassisNumber = "CHS007", RegistrationDate = DateTime.Now.AddYears(-4), LastServiceDate = DateTime.Now.AddMonths(-6), Mileage = 40000, FuelType = "Diesel", Transmission = "Manual", InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(7), RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false },
                    new Vehicle { Id = "V8", Make = "Honda", Model = "Crosstour", Year = 2022, VIN = "VIN008", LicensePlate = "JUH664", Color = "White", EngineNumber = "ENG008", ChassisNumber = "CHS008", RegistrationDate = DateTime.Now.AddMonths(-1), LastServiceDate = DateTime.Now.AddDays(-20), Mileage = 3000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(11), RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = false }

                    // Add additional vehicles as needed...
                };

                HttpContext.Session.SetString("Vehicles", JsonSerializer.Serialize(vehicles));
                return vehicles;
            }
            return JsonSerializer.Deserialize<List<Vehicle>>(json);
        }

        protected void SaveVehiclesToSession(List<Vehicle> vehicles)
        {
            HttpContext.Session.SetString("Vehicles", JsonSerializer.Serialize(vehicles));
        }

        protected string GetDriverNameById(string driverId)
        {
            var drivers = GetDriversFromSession();
            return drivers.FirstOrDefault(d => d.Id.ToString() == driverId)?.FullName ?? "";
        }
    }
}
