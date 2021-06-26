using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandWeb.Data;
using StandWeb.Models;

namespace StandWeb.Controllers.API {

   /// <summary>
   /// API controller para interagir com os dados dos carros
   /// </summary>
   [Route("api/[controller]")]
   [ApiController]
   public class CarrosAPIController : ControllerBase {
      private readonly GostosDB _context;

      public CarrosAPIController(GostosDB context) {
         _context = context;
      }

      /// <summary>
      /// lista os carros existentes na BD
      /// </summary>
      /// <returns></returns>
      // GET: api/CaesAPI
      [HttpGet]
      public async Task<ActionResult<IEnumerable<CarrosAPIViewModel>>> GetCaes() {


         var listaCarros = await _context.Carros
                                       .Select(c => new CarrosAPIViewModel {
                                          IdCarro = c.Id,
                                          NomeCarro = c.Modelo
                                       })
                                       .OrderBy(c=>c.NomeCarro)
                                       .ToListAsync();

         return listaCarros;
      }

      // GET: api/CarrosAPI/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Carros>> GetCarros(int id) {
         var carros = await _context.Carros.FindAsync(id);

         if (carros == null) {
            return NotFound();
         }

         return carros;
      }

      // PUT: api/CarrosAPI/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutCarros(int id, Carros carros) {
         if (id != carros.Id) {
            return BadRequest();
         }

         _context.Entry(carros).State = EntityState.Modified;

         try {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException) {
            if (!CarrosExists(id)) {
               return NotFound();
            }
            else {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/CaesAPI
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Carros>> PostCarros(Carros carros) {
         _context.Carros.Add(carros);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetCarros", new { id = carros.Id }, carros);
      }

      // DELETE: api/CarrosAPI/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteCarros(int id) {
         var carros = await _context.Carros.FindAsync(id);
         if (carros == null) {
            return NotFound();
         }

         _context.Carros.Remove(carros);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool CarrosExists(int id) {
         return _context.Carros.Any(e => e.Id == id);
      }
   }
}
