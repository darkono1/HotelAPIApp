using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace HotelAPI
{
    public class Hotel
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public float price { get; set; }    
        public float latitude { get; set; } 
        public float longitude { get; set; }


    }

    

}
