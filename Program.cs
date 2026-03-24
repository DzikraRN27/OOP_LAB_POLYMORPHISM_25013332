using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolymorphismLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> vehiclesForRent = new List<Vehicle>()
            {
                new Car("Nissan", "Grand Livina", 2010, 100.00, 4),
                new Motorcycle ("Yamaha", "N-Max", 2019, 80.00, false),
                new Truck("Isuzu", "IDK", 2016, 200.00, 7)
            };

            

            foreach (var v in vehiclesForRent)
            {
                double cost = v.CalculateRentalCost(5);
                Console.WriteLine(v.GetDescription());
                Console.WriteLine($"Cost of rent for 5 days: RM {cost}");
             
            }

            var expensive = vehiclesForRent.OrderByDescending(v => v.CalculateRentalCost(5)).First();
            Console.WriteLine($"Vehicle with the highest rent: {expensive.Make} {expensive.Model} {expensive.Year} - RM {expensive.CalculateRentalCost(5)}");

        }
    }

    public abstract class Vehicle
    {
        private string make;
        private string model;
        private int year;
        private double dailyRate;

        public string Make {
            get
            {
                return make;
            }
            set
            {
                make = value;
            }
        }

        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        public double DailyRate
        {
            get
            {
                return dailyRate;
            }
            set
            {
                if (value <= 0) throw new ArgumentException("Daily rate must be greater than zero");
                else dailyRate = value;
            }
        }

       

        public Vehicle(string make, string model, int year, double dailyRate)
        {
            this.make = make;
            this.model = model;
            this.year = year;
            this.dailyRate = dailyRate;
        }

        public abstract double CalculateRentalCost(int days);

        public virtual string GetDescription()
        {
            return $"{Year} {Make} {Model} - RM {DailyRate}/day";
        }
    }

    public class Car : Vehicle
    {
        private int numPassengers;

        public override double CalculateRentalCost(int days)
        {
            return DailyRate * days;
        }

        public override string GetDescription()
        {
            return $"{Year} {Make} {Model} {numPassengers} Passengers - RM {DailyRate}/day";
        }

        public Car(string make, string model, int year, double dailyRate, int numPassengers) : base(make, model, year, dailyRate)
        {
            this.numPassengers = numPassengers;
        }
    }

    public class Motorcycle : Vehicle
    {
        private bool hasSidecar;

        public override double CalculateRentalCost(int days)
        {
            if (!hasSidecar) return DailyRate * days * 0.9;
            else return DailyRate * days;
        }

        public override string GetDescription()
        {
            if(hasSidecar) return $"{Year} {Make} {Model} (with side car) - RM {DailyRate}/day";
            else return $"{Year} {Make} {Model} (no side car) - RM {DailyRate}/day";
        }

        public Motorcycle(string make, string model, int year, double dailyRate, bool hasSidecar) : base(make, model, year, dailyRate)
        {
            this.hasSidecar = hasSidecar;
        }
    }

    public class Truck : Vehicle
    {
        private double payloadTons;

        public override double CalculateRentalCost (int days)
        {
            double surcharge = 30 * payloadTons;
            return (DailyRate + surcharge) * days;
        }

        public override string GetDescription()
        {
            return $"{Year} {Make} {Model} ({payloadTons} tons)  - RM {DailyRate}/day";
        }

        public Truck(string make, string model, int year, double dailyRate, double payloadTons) : base(make, model, year, dailyRate)
        {
            this.payloadTons = payloadTons;
        }
    }
}
