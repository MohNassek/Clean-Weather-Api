using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi.Domain.Entities
{
    public class Weather
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }


    }
}
