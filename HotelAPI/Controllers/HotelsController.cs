using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Data;



namespace HotelAPI.Controllers
{
    public class OrderByLatLon : Hotel

    {


        public double distance;

        public static double calculateDist(double lat1, double lon1, double lat2, double lon2)
        {
            double distan;
            static double toRadians(
            double angleIn10thofaDegree)
            {
                // Angle in 10th
                // of a degree
                return (angleIn10thofaDegree *
                               Math.PI) / 180;
            }
            double distance(double lat1,
                             double lat2,
                             double lon1,
                             double lon2)
            {

                lon1 = toRadians(lon1);
                lon2 = toRadians(lon2);
                lat1 = toRadians(lat1);
                lat2 = toRadians(lat2);

                // Haversine formula
                double dlon = lon2 - lon1;
                double dlat = lat2 - lat1;
                double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                           Math.Cos(lat1) * Math.Cos(lat2) *
                           Math.Pow(Math.Sin(dlon / 2), 2);

                double c = 2 * Math.Asin(Math.Sqrt(a));


                double r = 6371; //radijus zemlje u km

                // calculate the result
                return (c * r);
            }

            // Driver code

            distan = (distance(lat1, lat2, lon1, lon2));

            return distan;

        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly DataContext _context;

        public HotelsController(DataContext context)
        {
            _context = context;
        }

        //GET: api/hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _context.Hotels.ToListAsync();
        }






        //GET: api/hotels/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        //GET: api/hotels/5
        [HttpGet("{latitude},{longitude}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
            return await _context.Hotels.ToListAsync();
        }



        //PUT: api/hotels/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<ActionResult> PutHotel(int  id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
    }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostInspection(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotels", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
