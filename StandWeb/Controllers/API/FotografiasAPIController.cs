﻿using System;
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
   /// Controller API que interage com os dados das Fotografias
   /// </summary>
   [Route("api/[controller]")]
   [ApiController]
   public class FotografiasAPIController : ControllerBase {
      private readonly GostosDB _context;

      public FotografiasAPIController(GostosDB context) {
         _context = context;
      }

      // GET: api/FotografiasAPI
      /// <summary>
      /// 'Endpoint' a apresentar os dados da Fotografias,
      ///  para integrar com a app REACT
      ///  Naturalmente, este 'endpoint' poderá ser acedido por outra aplicação qualquer...
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public async Task<ActionResult<IEnumerable<FotosAPIViewModel>>> GetFotografias() {
         return await _context.Fotografias
                              .Include(f => f.Carro)
                              .Select(f => new FotosAPIViewModel {
                                 IdFoto = f.Id,
                                 NomeFoto = f.Fotografia,
                                 LocalFoto = f.Local,
                                 DataFoto = f.DataFoto.ToShortDateString(),
                                 ModeloCarro = f.Carro.Modelo
                              })
                              .ToListAsync();
      }

      // GET: api/FotografiasAPI/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Fotografias>> GetFotografias(int id) {
         var fotografias = await _context.Fotografias.FindAsync(id);

         if (fotografias == null) {
            return NotFound();
         }

         return fotografias;
      }

      // PUT: api/FotografiasAPI/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutFotografias(int id, Fotografias fotografias) {
         if (id != fotografias.Id) {
            return BadRequest();
         }

         _context.Entry(fotografias).State = EntityState.Modified;

         try {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException) {
            if (!FotografiasExists(id)) {
               return NotFound();
            }
            else {
               throw;
            }
         }

         return NoContent();
      }

      /// <summary>
      /// Endpoint para receber os dados do Formulário de adição de novas fotografias
      /// </summary>
      /// <param name="fotografia">dados da nova fotografia</param>
      /// <param name="UploadFotografia">ficheiro com a imagem da fotografia</param>
      /// <returns></returns>
      // POST: api/FotografiasAPI
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Fotografias>> PostFotografias([FromForm]Fotografias fotografia, IFormFile UploadFotografia) {

         /* - o anotador [FromForm] instrui a ASP .NET Core a aceitar os dados vindos do formulário do React
          *   e associá-los ao objeto interno 'fotografia'
          * 
          * - o atributo UploadFotografia terá um tratamento 100% igual ao que foi feito no controller das Fotografias
          */

         // *********************************************************************
         // esta instrução é apenas usada para não se criar uma exceção no código
         // deverá ser apagada quando se concretizar o trabalho real
         fotografia.Fotografia = "";
         // *********************************************************************

         _context.Fotografias.Add(fotografia);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetFotografias", new { id = fotografia.Id }, fotografia);
      }

      // DELETE: api/FotografiasAPI/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteFotografias(int id) {
         var fotografias = await _context.Fotografias.FindAsync(id);
         if (fotografias == null) {
            return NotFound();
         }

         _context.Fotografias.Remove(fotografias);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool FotografiasExists(int id) {
         return _context.Fotografias.Any(e => e.Id == id);
      }
   }
}
