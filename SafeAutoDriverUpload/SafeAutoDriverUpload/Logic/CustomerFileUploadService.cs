using Microsoft.AspNetCore.Http;
using SafeAutoDriverUpload.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SafeAutoDriverUpload.Logic
{
    public class CustomerFileUploadService : ICustomerFileUploadService
    {

        private List<Driver> ReadFile(IFormFile file)
        {
            List<Driver> drivers = new List<Driver>();

            try
            {
                using (var sr = new StreamReader(file.OpenReadStream()))
                {
                    while (!sr.EndOfStream)
                    {
                        string unsplit = sr.ReadLine();
                        string[] split = unsplit.Split(' ');

                        if (split[0] == "Driver")
                        {
                            Driver tempDriver = new Driver();
                            tempDriver.Command = split[0];
                            tempDriver.Name = split[1];
                            drivers.Add(tempDriver);
                        }
                        else if (split[0] == "Trip")
                        {
                            Trip tempTrip = new Trip();
                            tempTrip.Command = split[0];
                            tempTrip.Name = split[1];
                            tempTrip.TripStartTime = DateTime.Parse(split[2]);
                            tempTrip.TripEndTime = DateTime.Parse(split[3]);
                            tempTrip.MilesDriven = double.Parse(split[4]);

                            foreach (Driver driver in drivers)
                            {
                                if (tempTrip.Name == driver.Name)
                                {
                                    driver.Trips.Add(tempTrip);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                drivers = new List<Driver>();
            }

            return drivers;
        }

        public List<DriverDto> UploadFile(IFormFile file)
        {
            var drivers = ReadFile(file);

            var dtos = new List<DriverDto>();



            foreach (Driver driver in drivers)
            {
                var dto = new DriverDto();
                double totalMiles = 0;
                double totalHours = 0;

                foreach (Trip data in driver.Trips)
                {
                    TimeSpan timeDriving = data.TripEndTime - data.TripStartTime;
                   double hours = timeDriving.TotalMinutes / 60;
                    if (data.MilesDriven / hours >= 5 && data.MilesDriven / hours <= 100)
                    {
                        totalMiles += data.MilesDriven;
                        totalHours += hours;
                    }

                }

                double mph = totalHours > 0 ? totalMiles / totalHours : 0;
                double roundedMph = Math.Round(mph);
                double roundedMiles = Math.Round(totalMiles);
                dto.Name = driver.Name;
                dto.Miles = roundedMiles;
                dto.Mph = roundedMph;
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
