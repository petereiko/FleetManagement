using FleetManagement.UI.Models.CompanyAssetDto;
using FleetManagement.UI.Models.DriverDTOs;
using FleetManagement.UI.Models.ServiceHubs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class BaseController : Controller
    {
        private readonly MasterVehicleList _masterList;
        protected List<Driver> GetDriversFromSession()
        {
            var json = HttpContext.Session.GetString("Drivers");
            if (string.IsNullOrEmpty(json))
            {
                var random = new Random();
                var drivers = new List<Driver>
                {
                    new Driver
                    {
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
                        Relationship = "Brother",
                        ShiftStatus = "On Duty",
                        LastSeen = DateTime.Now
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
                        Relationship = "Mother",
                        ShiftStatus = "Available",
                        LastSeen = DateTime.Now.AddMinutes(-new Random().Next(15, 31))
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
                        Relationship = "Cousin",
                        ShiftStatus = "Off Duty",
                        LastSeen = DateTime.Now.AddHours(-new Random().Next(1, 4))
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
                        Relationship = "Brother",
                        ShiftStatus = "Available",
                        LastSeen = DateTime.Now.AddMinutes(-new Random().Next(15, 31))
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
                        Relationship = "Father",
                        ShiftStatus = "On Duty",
                        LastSeen = DateTime.Now
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
                        Relationship = "Wife",
                        ShiftStatus = "Off Duty",
                        LastSeen = DateTime.Now.AddHours(-new Random().Next(1, 4))
                    },
                    new Driver {
                        Id = "D7",
                        FirstName = "Blessing",
                        MiddleName = "Oluwatosin",
                        LastName = "Ajayi",
                        Email = "blessing@example.com",
                        PhoneNumber = "08078904561",
                        Address = "12 Palm Grove, Lagos",
                        DateOfBirth = new DateTime(1993, 3, 12),
                        Gender = "Female",
                        LicenseNumber = "LIC11223",
                        LicenseExpiryDate = DateTime.Now.AddYears(2),
                        LicenseCategory = "Class B",
                        DateHired = DateTime.Now.AddYears(-3),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Samuel Ajayi",
                        EmergencyContactPhone = "08099887766",
                        Relationship = "Husband",
                        ShiftStatus = "Available",
                        LastSeen = DateTime.Now.AddMinutes(-random.Next(15, 31))
                    },
                    new Driver 
                    {
                        Id = "D8",
                        FirstName = "Isaac",
                        MiddleName = "Kelechi",
                        LastName = "Umeh",
                        Email = "isaac@example.com",
                        PhoneNumber = "08034569876",
                        Address = "3 Iweka Rd, Onitsha",
                        DateOfBirth = new DateTime(1989, 9, 5),
                        Gender = "Male",
                        LicenseNumber = "LIC99887",
                        LicenseExpiryDate = DateTime.Now.AddYears(1),
                        LicenseCategory = "Class A",
                        DateHired = DateTime.Now.AddYears(-2),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Ngozi Umeh",
                        EmergencyContactPhone = "08034561234",
                        Relationship = "Sister",
                        ShiftStatus = "Off Duty",
                        LastSeen = DateTime.Now.AddHours(-random.Next(1, 4))
                    },
                    new Driver {
                        Id = "D9",
                        FirstName = "Chinyere",
                        MiddleName = "Amaka",
                        LastName = "Obi",
                        Email = "chinyere@example.com",
                        PhoneNumber = "08012349876",
                        Address = "6 Garden Lane, Owerri",
                        DateOfBirth = new DateTime(1991, 2, 17),
                        Gender = "Female",
                        LicenseNumber = "LIC33445",
                        LicenseExpiryDate = DateTime.Now.AddYears(3),
                        LicenseCategory = "Class C",
                        DateHired = DateTime.Now.AddYears(-1),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Peter Obi",
                        EmergencyContactPhone = "08045671234",
                        Relationship = "Brother",
                        ShiftStatus = "On Duty",
                        LastSeen = DateTime.Now
                    },
                    new Driver {
                        Id = "D10",
                        FirstName = "Tunde",
                        MiddleName = "Olamide",
                        LastName = "Adekunle",
                        Email = "tunde@example.com",
                        PhoneNumber = "08099887766",
                        Address = "21 Iwo Rd, Ibadan",
                        DateOfBirth = new DateTime(1986, 6, 25),
                        Gender = "Male",
                        LicenseNumber = "LIC55667",
                        LicenseExpiryDate = DateTime.Now.AddYears(2),
                        LicenseCategory = "Class B",
                        DateHired = DateTime.Now.AddYears(-4),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Yinka Adekunle",
                        EmergencyContactPhone = "08033445566",
                        Relationship = "Wife",
                        ShiftStatus = "Available",
                        LastSeen = DateTime.Now.AddMinutes(-random.Next(15, 31))
                    },
                    new Driver {
                        Id = "D11",
                        FirstName = "Aisha",
                        MiddleName = "Halima",
                        LastName = "Abubakar",
                        Email = "aisha@example.com",
                        PhoneNumber = "08066778899",
                        Address = "8 Zaria Way, Kano",
                        DateOfBirth = new DateTime(1994, 11, 3),
                        Gender = "Female",
                        LicenseNumber = "LIC77889",
                        LicenseExpiryDate = DateTime.Now.AddYears(3),
                        LicenseCategory = "Class A",
                        DateHired = DateTime.Now.AddYears(-2),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Usman Abubakar",
                        EmergencyContactPhone = "08055667788",
                        Relationship = "Father",
                        ShiftStatus = "Off Duty",
                        LastSeen = DateTime.Now.AddHours(-random.Next(1, 4))
                    },
                    new Driver {
                        Id = "D12",
                        FirstName = "David",
                        MiddleName = "Tijani",
                        LastName = "Bashir",
                        Email = "david@example.com",
                        PhoneNumber = "08033445522",
                        Address = "17 Ring Road, Abuja",
                        DateOfBirth = new DateTime(1983, 1, 20),
                        Gender = "Male",
                        LicenseNumber = "LIC88990",
                        LicenseExpiryDate = DateTime.Now.AddYears(2),
                        LicenseCategory = "Class C",
                        DateHired = DateTime.Now.AddYears(-6),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Rufai Bashir",
                        EmergencyContactPhone = "08044556677",
                        Relationship = "Brother",
                        ShiftStatus = "Available",
                        LastSeen = DateTime.Now.AddMinutes(-random.Next(15, 31))
                    },
                    new Driver {
                        Id = "D13",
                        FirstName = "Ngozi",
                        MiddleName = "Chika",
                        LastName = "Eze",
                        Email = "ngozi@example.com",
                        PhoneNumber = "08055664433",
                        Address = "5 Unity Estate, Asaba",
                        DateOfBirth = new DateTime(1996, 10, 14),
                        Gender = "Female",
                        LicenseNumber = "LIC11234",
                        LicenseExpiryDate = DateTime.Now.AddYears(1),
                        LicenseCategory = "Class B",
                        DateHired = DateTime.Now.AddYears(-2),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Chinedu Eze",
                        EmergencyContactPhone = "08066778899",
                        Relationship = "Husband",
                        ShiftStatus = "On Duty",
                        LastSeen = DateTime.Now
                    },
                    new Driver {
                        Id = "D14",
                        FirstName = "Yakubu",
                        MiddleName = "Mohammed",
                        LastName = "Lawal",
                        Email = "yakubu@example.com",
                        PhoneNumber = "08099881234",
                        Address = "13 Emir Road, Minna",
                        DateOfBirth = new DateTime(1982, 12, 1),
                        Gender = "Male",
                        LicenseNumber = "LIC66778",
                        LicenseExpiryDate = DateTime.Now.AddYears(3),
                        LicenseCategory = "Class A",
                        DateHired = DateTime.Now.AddYears(-7),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Binta Lawal",
                        EmergencyContactPhone = "08055667733",
                        Relationship = "Wife",
                        ShiftStatus = "Off Duty",
                        LastSeen = DateTime.Now.AddHours(-random.Next(1, 4))
                    },
                    new Driver {
                        Id = "D15",
                        FirstName = "Ruth",
                        MiddleName = "Ada",
                        LastName = "Ifeoma",
                        Email = "ruth@example.com",
                        PhoneNumber = "08044556633",
                        Address = "19 Goodwill Rd, Makurdi",
                        DateOfBirth = new DateTime(1997, 5, 30),
                        Gender = "Female",
                        LicenseNumber = "LIC77882",
                        LicenseExpiryDate = DateTime.Now.AddYears(2),
                        LicenseCategory = "Class B",
                        DateHired = DateTime.Now.AddYears(-1),
                        EmploymentStatus = "Active",
                        EmergencyContactName = "Esther Ifeoma",
                        EmergencyContactPhone = "08088990011",
                        Relationship = "Sister",
                        ShiftStatus = "Available",
                        LastSeen = DateTime.Now.AddMinutes(-random.Next(15, 31))
                    }
                };

                HttpContext.Session.SetString("Drivers", JsonSerializer.Serialize(drivers));
                return drivers;
            }

            return JsonSerializer.Deserialize<List<Driver>>(json);
        }

        protected List<Vehicle> GetVehiclesFromSession()
        {
            var json = HttpContext.Session.GetString("Vehicles");
            List<Vehicle> vehicles;
            if (string.IsNullOrEmpty(json))
            {
                vehicles = new List<Vehicle>
                {
                    new Vehicle {
                            Id = "V1", Make = "Toyota", Model = "Camry", Year = 2020, VIN = "VIN001", LicensePlate = "ABC123", Color = "Silver",
                            EngineNumber = "ENG001", ChassisNumber = "CHS001", RegistrationDate = DateTime.Now.AddYears(-1),
                            LastServiceDate = DateTime.Now.AddMonths(-3), Mileage = 15000, FuelType = "Petrol", Transmission = "Automatic",
                            InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(6),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = true,
                            AssignedDriverId = "D1", AssignedDriverName = "Eddie Hoyte", Latitude = 6.5244, Longitude = 3.3792 // Lagos
                        },
                    new Vehicle {
                            Id = "V2", Make = "Honda", Model = "Civic", Year = 2019, VIN = "VIN002", LicensePlate = "XYZ789", Color = "Blue",
                            EngineNumber = "ENG002", ChassisNumber = "CHS002", RegistrationDate = DateTime.Now.AddYears(-2),
                            LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 20000, FuelType = "Petrol", Transmission = "Manual",
                            InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(8),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(5), Status = "Active", IsAssigned = true,
                            AssignedDriverId = "D2", AssignedDriverName = "Jane Smith", Latitude = 9.0579, Longitude = 7.4951 // Abuja
                        },
                    new Vehicle {
                            Id = "V3", Make = "Ford", Model = "Focus", Year = 2021, VIN = "VIN003", LicensePlate = "FOC456", Color = "Blue",
                            EngineNumber = "ENG003", ChassisNumber = "CHS003", RegistrationDate = DateTime.Now.AddMonths(-10),
                            LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 10000, FuelType = "Diesel", Transmission = "Automatic",
                            InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(10),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = true,
                            AssignedDriverId = "D3", AssignedDriverName = "Gbenga Tokunbo", Latitude = 4.8156, Longitude = 7.0498 // Port Harcourt
                        },
                    new Vehicle {
                            Id = "V4", Make = "Chevrolet", Model = "Malibu", Year = 2018, VIN = "VIN004", LicensePlate = "MAL321", Color = "Black",
                            EngineNumber = "ENG004", ChassisNumber = "CHS004", RegistrationDate = DateTime.Now.AddYears(-3),
                            LastServiceDate = DateTime.Now.AddMonths(-4), Mileage = 30000, FuelType = "Petrol", Transmission = "Automatic",
                            InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(5),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = false,
                            Latitude = 6.4531, Longitude = 3.3958 // Victoria Island
                        },
                    new Vehicle {
                            Id = "V5", Make = "Nissan", Model = "Altima", Year = 2022, VIN = "VIN005", LicensePlate = "ALT654", Color = "Red",
                            EngineNumber = "ENG005", ChassisNumber = "CHS005", RegistrationDate = DateTime.Now.AddMonths(-2),
                            LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 5000, FuelType = "Petrol", Transmission = "Automatic",
                            InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(12),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(7), Status = "Active", IsAssigned = false,
                            Latitude = 7.3775, Longitude = 3.9470 // Ibadan
                        },
                    new Vehicle {
                            Id = "V6", Make = "Mercedes Benz", Model = "E360", Year = 2022, VIN = "VIN006", LicensePlate = "GBV856", Color = "Gray",
                            EngineNumber = "ENG006", ChassisNumber = "CHS006", RegistrationDate = DateTime.Now.AddMonths(-3),
                            LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 8000, FuelType = "Diesel", Transmission = "Automatic",
                            InsuranceCompany = "PremiumInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(9),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(8), Status = "Active", IsAssigned = false,
                            Latitude = 6.1730, Longitude = 6.7884 // Asaba
                        },
                    new Vehicle {
                            Id = "V7", Make = "Toyota", Model = "Tundra", Year = 2018, VIN = "VIN007", LicensePlate = "VALL423", Color = "Red",
                            EngineNumber = "ENG007", ChassisNumber = "CHS007", RegistrationDate = DateTime.Now.AddYears(-4),
                            LastServiceDate = DateTime.Now.AddMonths(-6), Mileage = 40000, FuelType = "Diesel", Transmission = "Manual",
                            InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(7),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false,
                            Latitude = 7.7697, Longitude = 5.5096 // Akure
                        },
                    new Vehicle {
                            Id = "V8", Make = "Honda", Model = "Crosstour", Year = 2022, VIN = "VIN008", LicensePlate = "JUH664", Color = "White",
                            EngineNumber = "ENG008", ChassisNumber = "CHS008", RegistrationDate = DateTime.Now.AddMonths(-1),
                            LastServiceDate = DateTime.Now.AddDays(-20), Mileage = 3000, FuelType = "Petrol", Transmission = "Automatic",
                            InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(11),
                            RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = false,
                            Latitude = 5.6037, Longitude = -0.1870 // Accra (for variety)
                        },
                    new Vehicle {
                        Id = "V9", Make = "Hyundai", Model = "Elantra", Year = 2020, VIN = "VIN009", LicensePlate = "HYU909", Color = "Blue",
                        EngineNumber = "ENG009", ChassisNumber = "CHS009", RegistrationDate = DateTime.Now.AddYears(-1),
                        LastServiceDate = DateTime.Now.AddMonths(-3), Mileage = 14000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(6),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D9", AssignedDriverName = "Chinyere Obi", Latitude = 6.5244, Longitude = 3.3792
                    },
                    new Vehicle {
                        Id = "V10", Make = "Kia", Model = "Rio", Year = 2021, VIN = "VIN010", LicensePlate = "KIA101", Color = "Gray",
                        EngineNumber = "ENG010", ChassisNumber = "CHS010", RegistrationDate = DateTime.Now.AddYears(-2),
                        LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 12000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(7),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D10", AssignedDriverName = "Tunde Adekunle", Latitude = 6.5244, Longitude = 3.3792
                    },
                    new Vehicle {
                        Id = "V11", Make = "Mazda", Model = "CX-5", Year = 2022, VIN = "VIN011", LicensePlate = "CXF212", Color = "Red",
                        EngineNumber = "ENG011", ChassisNumber = "CHS011", RegistrationDate = DateTime.Now.AddMonths(-10),
                        LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 7000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(10),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D11", AssignedDriverName = "Aisha Halima", Latitude = 6.4531, Longitude = 3.3958
                    },
                    new Vehicle {
                        Id = "V12", Make = "Peugeot", Model = "508", Year = 2019, VIN = "VIN012", LicensePlate = "PEU508", Color = "Gray",
                        EngineNumber = "ENG012", ChassisNumber = "CHS012", RegistrationDate = DateTime.Now.AddYears(-4),
                        LastServiceDate = DateTime.Now.AddMonths(-5), Mileage = 25000, FuelType = "Diesel", Transmission = "Manual",
                        InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(5),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(2), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D12", AssignedDriverName = "David Tijani", Latitude = 7.3775, Longitude = 3.9470
                    },
                    new Vehicle {
                        Id = "V13", Make = "Volkswagen", Model = "Passat", Year = 2020, VIN = "VIN013", LicensePlate = "VW0032", Color = "Silver",
                        EngineNumber = "ENG013", ChassisNumber = "CHS013", RegistrationDate = DateTime.Now.AddYears(-1),
                        LastServiceDate = DateTime.Now.AddMonths(-4), Mileage = 16000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "PremiumInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(8),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D13", AssignedDriverName = "Ngozi Eze", Latitude = 6.1730, Longitude = 6.7884
                    },
                    new Vehicle {
                        Id = "V14", Make = "BMW", Model = "X3", Year = 2021, VIN = "VIN014", LicensePlate = "BMWX3", Color = "Black",
                        EngineNumber = "ENG014", ChassisNumber = "CHS014", RegistrationDate = DateTime.Now.AddMonths(-9),
                        LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 13000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(6),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D14", AssignedDriverName = "Yakubu Mohammed", Latitude = 7.7697, Longitude = 5.5096
                    },
                    new Vehicle {
                        Id = "V15", Make = "Audi", Model = "A4", Year = 2022, VIN = "VIN015", LicensePlate = "AUD015", Color = "White",
                        EngineNumber = "ENG015", ChassisNumber = "CHS015", RegistrationDate = DateTime.Now.AddMonths(-5),
                        LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 8000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(12),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = true,
                        AssignedDriverId = "D15", AssignedDriverName = "Ruth Ada Ifeoma", Latitude = 5.6037, Longitude = -0.1870
                    },
                    new Vehicle {
                        Id = "V16", Make = "Suzuki", Model = "Swift", Year = 2018, VIN = "VIN016", LicensePlate = "SWI116", Color = "Blue",
                        EngineNumber = "ENG016", ChassisNumber = "CHS016", RegistrationDate = DateTime.Now.AddYears(-4),
                        LastServiceDate = DateTime.Now.AddMonths(-3), Mileage = 35000, FuelType = "Petrol", Transmission = "Manual",
                        InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(9),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false,
                        Latitude = 6.4654, Longitude = 3.4064
                    },
                    new Vehicle {
                        Id = "V17", Make = "Renault", Model = "Logan", Year = 2017, VIN = "VIN017", LicensePlate = "LOG717", Color = "Green",
                        EngineNumber = "ENG017", ChassisNumber = "CHS017", RegistrationDate = DateTime.Now.AddYears(-5),
                        LastServiceDate = DateTime.Now.AddMonths(-6), Mileage = 45000, FuelType = "Diesel", Transmission = "Manual",
                        InsuranceCompany = "PremiumInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(5),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = false,
                        Latitude = 6.5244, Longitude = 3.3792
                    },
                    new Vehicle {
                        Id = "V18", Make = "Jeep", Model = "Compass", Year = 2020, VIN = "VIN018", LicensePlate = "CMP818", Color = "Gray",
                        EngineNumber = "ENG018", ChassisNumber = "CHS018", RegistrationDate = DateTime.Now.AddYears(-2),
                        LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 18000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(7),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false,
                        Latitude = 6.5244, Longitude = 3.3792
                    },
                    new Vehicle {
                        Id = "V19", Make = "Changan", Model = "CS35", Year = 2021, VIN = "VIN019", LicensePlate = "CHA119", Color = "White",
                        EngineNumber = "ENG019", ChassisNumber = "CHS019", RegistrationDate = DateTime.Now.AddYears(-1),
                        LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 12000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(8),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false,
                        Latitude = 6.5346, Longitude = 3.3790
                    },
                    new Vehicle {
                        Id = "V20", Make = "Geely", Model = "Coolray", Year = 2022, VIN = "VIN020", LicensePlate = "COO220", Color = "Red",
                        EngineNumber = "ENG020", ChassisNumber = "CHS020", RegistrationDate = DateTime.Now.AddMonths(-7),
                        LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 9000, FuelType = "Petrol", Transmission = "Automatic",
                        InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(9),
                        RoadWorthyExpiryDate = DateTime.Now.AddMonths(5), Status = "Active", IsAssigned = false,
                        Latitude = 6.5480, Longitude = 3.3380
                    }
                };

                HttpContext.Session.SetString("Vehicles", JsonSerializer.Serialize(vehicles));
            }
            else
            {
                vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json);
            }

            MasterVehicleList.VehicleStore.Vehicles = vehicles;
            return vehicles;
        }


        //protected List<Vehicle> GetVehiclesFromSession()
        //{
        //    var json = HttpContext.Session.GetString("Vehicles");
        //    if (string.IsNullOrEmpty(json)) 
        //    {
        //        var vehicles = new List<Vehicle>
        //        {
        //            new Vehicle {
        //                    Id = "V1", Make = "Toyota", Model = "Camry", Year = 2020, VIN = "VIN001", LicensePlate = "ABC123", Color = "White",
        //                    EngineNumber = "ENG001", ChassisNumber = "CHS001", RegistrationDate = DateTime.Now.AddYears(-1),
        //                    LastServiceDate = DateTime.Now.AddMonths(-3), Mileage = 15000, FuelType = "Petrol", Transmission = "Automatic",
        //                    InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(6),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = true,
        //                    AssignedDriverId = "D1", AssignedDriverName = "Eddie Hoyte", Latitude = 6.5244, Longitude = 3.3792 // Lagos
        //                },
        //            new Vehicle {
        //                    Id = "V2", Make = "Honda", Model = "Civic", Year = 2019, VIN = "VIN002", LicensePlate = "XYZ789", Color = "Black",
        //                    EngineNumber = "ENG002", ChassisNumber = "CHS002", RegistrationDate = DateTime.Now.AddYears(-2),
        //                    LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 20000, FuelType = "Petrol", Transmission = "Manual",
        //                    InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(8),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(5), Status = "Active", IsAssigned = true,
        //                    AssignedDriverId = "D2", AssignedDriverName = "Jane Smith", Latitude = 9.0579, Longitude = 7.4951 // Abuja
        //                },
        //            new Vehicle {
        //                    Id = "V3", Make = "Ford", Model = "Focus", Year = 2021, VIN = "VIN003", LicensePlate = "FOC456", Color = "Blue",
        //                    EngineNumber = "ENG003", ChassisNumber = "CHS003", RegistrationDate = DateTime.Now.AddMonths(-10),
        //                    LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 10000, FuelType = "Diesel", Transmission = "Automatic",
        //                    InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(10),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = true,
        //                    AssignedDriverId = "D3", AssignedDriverName = "Gbenga Tokunbo", Latitude = 4.8156, Longitude = 7.0498 // Port Harcourt
        //                },
        //            new Vehicle {
        //                    Id = "V4", Make = "Chevrolet", Model = "Malibu", Year = 2018, VIN = "VIN004", LicensePlate = "MAL321", Color = "Silver",
        //                    EngineNumber = "ENG004", ChassisNumber = "CHS004", RegistrationDate = DateTime.Now.AddYears(-3),
        //                    LastServiceDate = DateTime.Now.AddMonths(-4), Mileage = 30000, FuelType = "Petrol", Transmission = "Automatic",
        //                    InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(5),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = false,
        //                    Latitude = 6.4531, Longitude = 3.3958 // Victoria Island
        //                },
        //            new Vehicle {
        //                    Id = "V5", Make = "Nissan", Model = "Altima", Year = 2022, VIN = "VIN005", LicensePlate = "ALT654", Color = "Red",
        //                    EngineNumber = "ENG005", ChassisNumber = "CHS005", RegistrationDate = DateTime.Now.AddMonths(-2),
        //                    LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 5000, FuelType = "Petrol", Transmission = "Automatic",
        //                    InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(12),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(7), Status = "Active", IsAssigned = false,
        //                    Latitude = 7.3775, Longitude = 3.9470 // Ibadan
        //                },
        //            new Vehicle {
        //                    Id = "V6", Make = "Mercedes Benz", Model = "E360", Year = 2022, VIN = "VIN006", LicensePlate = "GBV856", Color = "Gray",
        //                    EngineNumber = "ENG006", ChassisNumber = "CHS006", RegistrationDate = DateTime.Now.AddMonths(-3),
        //                    LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 8000, FuelType = "Diesel", Transmission = "Automatic",
        //                    InsuranceCompany = "PremiumInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(9),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(8), Status = "Active", IsAssigned = false,
        //                    Latitude = 6.1730, Longitude = 6.7884 // Asaba
        //                },
        //            new Vehicle {
        //                    Id = "V7", Make = "Toyota", Model = "Tundra", Year = 2018, VIN = "VIN007", LicensePlate = "VALL423", Color = "Blue",
        //                    EngineNumber = "ENG007", ChassisNumber = "CHS007", RegistrationDate = DateTime.Now.AddYears(-4),
        //                    LastServiceDate = DateTime.Now.AddMonths(-6), Mileage = 40000, FuelType = "Diesel", Transmission = "Manual",
        //                    InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(7),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false,
        //                    Latitude = 7.7697, Longitude = 5.5096 // Akure
        //                },
        //            new Vehicle {
        //                    Id = "V8", Make = "Honda", Model = "Crosstour", Year = 2022, VIN = "VIN008", LicensePlate = "JUH664", Color = "White",
        //                    EngineNumber = "ENG008", ChassisNumber = "CHS008", RegistrationDate = DateTime.Now.AddMonths(-1),
        //                    LastServiceDate = DateTime.Now.AddDays(-20), Mileage = 3000, FuelType = "Petrol", Transmission = "Automatic",
        //                    InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(11),
        //                    RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = false,
        //                    Latitude = 5.6037, Longitude = -0.1870 // Accra (for variety)
        //                }
        //        };

        //        HttpContext.Session.SetString("Vehicles", JsonSerializer.Serialize(vehicles));
        //        return vehicles;
        //    }
        //    return JsonSerializer.Deserialize<List<Vehicle>>(json);
        //}

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
