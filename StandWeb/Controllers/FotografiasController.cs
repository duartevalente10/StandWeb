﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandWeb.Data;
using StandWeb.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace StandWeb.Controllers
{

    [Authorize] // esta 'anotação' garante que só as pessoas autenticadas têm acesso aos recursos
    public class FotografiasController : Controller
    {

        /// <summary>
        /// este atributo representa a base de dados do projeto
        /// </summary>
        private readonly GostosDB _context;

        /// <summary>
        /// este atributo contém os dados da app web no servidor
        /// </summary>
        private readonly IWebHostEnvironment _caminho;

        /// <summary>
        /// esta variável recolhe os dados da pessoa q se autenticou
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

         public FotografiasController(
           GostosDB context,
           IWebHostEnvironment caminho,
           UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        /// <summary>
        /// Mostra uma lista de imagens dos carros dos clientes
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] // anula a necessidade de um utilizador estar autenticado
                         // para aceder a este método
        public async Task<IActionResult> Index()
        {

            // dados de todas as fotografias
            var fotografias = await _context.Fotografias.Include(f => f.Carro).ToListAsync();

            // var. auxiliar
            string idDaPessoaAutenticada = _userManager.GetUserId(User);

            // quais os carros que pertencem à pessoa que está autenticada?
            // quais os seus IDs?
            var carros = await (from c in _context.Carros
                              join cc in _context.Gostos on c.Id equals cc.CarroFK
                              join cr in _context.Clientes on cc.ClienteFK equals cr.Id
                              where cr.UserName == idDaPessoaAutenticada
                              select c.Id)
                             .ToListAsync();

            // transportar os dois objetos para a View
            // iremos usar um ViewModel
            var fotos = new FotosCarros
            {
                ListaCarros = carros,
                ListaFotografias = fotografias
            };

            // invoca a View, entregando-lhe a lista de registos das fotografias e dos carros
            return View(fotos);
        }

        // GET: Fotografias/Details/5
        /// <summary>
        /// Mostra os detalhes de uma fotografia
        /// </summary>
        /// <param name="id">Identificador da Fotografia</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                // entro aqui se não foi especificado o ID

                // redirecionar para a página de início
                return RedirectToAction("Index");

                //return NotFound();
            }

            // se chego aqui, foi especificado um ID
            // vou procurar se existe uma Fotografia com esse valor
            var fotografia = await _context.Fotografias
                                           .Include(f => f.Carro)
                                           .FirstOrDefaultAsync(f => f.Id == id);

            if (fotografia == null)
            {
                // o ID especificado não corresponde a uma fotografia

                // return NotFound();
                // redirecionar para a página de início
                return RedirectToAction("Index");
            }

            // se cheguei aqui, é pq a foto existe e foi encontrada
            // então, mostro-a na View
            return View(fotografia);
        }

        // GET: Fotografias/Create
        /// <summary>
        /// invoca, na primeira vez, a View com os dados de criação de uma fotografia
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {

            ViewData["CarroFK"] = new SelectList(_context.Carros.OrderBy(c => c.Modelo), "Id", "Modelo");


            return View();
        }

        // POST: Fotografias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataFoto,Local,CarroFK")] Fotografias foto, IFormFile fotoCarro)
        {

            // avaliar se  o utilizador escolheu uma opção válida na dropdown do Cão
            if (foto.CarroFK < 0)
            {
                // não foi escolhido um cão válido 
                ModelState.AddModelError("", "Não se esqueça de escolher um carro...");
                // devolver o controlo à View
                ViewData["CarroFK"] = new SelectList(_context.Carros.OrderBy(c => c.Modelo), "Id", "Modelo");
                return View(foto);
            }

            /* processar o ficheiro
             *   - existe ficheiro?
             *     - se não existe, o q fazer?  => gerar uma msg erro, e devolver controlo à View
             *     - se continuo, é pq ficheiro existe
             *       - mas, será q é do tipo correto?
             *         - avaliar se é imagem,
             *           - se sim: - especificar o seu novo nome
             *                     - associar ao objeto 'foto', o nome deste ficheiro
             *                     - especificar a localização                     
             *                     - guardar ficheiro no disco rígido do servidor
             *           - se não  => gerar uma msg erro, e devolver controlo à View
            */

            // var auxiliar
            string nomeImagem = "";

            if (fotoCarro == null)
            {
                // não há ficheiro
                // adicionar msg de erro
                ModelState.AddModelError("", "Adicione, por favor, a fotografia do carro");
                // devolver o controlo à View
                ViewData["CarroFK"] = new SelectList(_context.Carros.OrderBy(c => c.Modelo), "Id", "Modelo");
                return View(foto);
            }
            else
            {
                // há ficheiro. Mas, será um ficheiro válido?
                // https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Basics_of_HTTP/MIME_types
                if (fotoCarro.ContentType == "image/jpeg" || fotoCarro.ContentType == "image/png")
                {
                    // definir o novo nome da fotografia     
                    Guid g;
                    g = Guid.NewGuid();
                    nomeImagem = foto.CarroFK + "_" + g.ToString(); // tb, poderia ser usado a formatação da data atual
                                                                  // determinar a extensão do nome da imagem
                    string extensao = Path.GetExtension(fotoCarro.FileName).ToLower();
                    // agora, consigo ter o nome final do ficheiro
                    nomeImagem = nomeImagem + extensao;

                    // associar este ficheiro aos dados da Fotografia do cão
                    foto.Fotografia = nomeImagem;

                    // localização do armazenamento da imagem
                    string localizacaoFicheiro = _caminho.WebRootPath;
                    nomeImagem = Path.Combine(localizacaoFicheiro, "fotos", nomeImagem);
                }
                else
                {
                    // ficheiro não é válido
                    // adicionar msg de erro
                    ModelState.AddModelError("", "Só pode escolher uma imagem para a associar ao carro");
                    // devolver o controlo à View
                    ViewData["CarroFK"] = new SelectList(_context.Carros.OrderBy(c => c.Modelo), "Id", "Modelo");
                    return View(foto);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // adicionar os dados da nova fotografia à base de dados
                    _context.Add(foto);
                    // consolidar os dados na base de dados
                    await _context.SaveChangesAsync();

                    // se cheguei aqui, tudo correu bem
                    // vou guardar, agora, no disco rígido do Servidor a imagem
                    using var stream = new FileStream(nomeImagem, FileMode.Create);
                    await fotoCarro.CopyToAsync(stream);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro...");

                }
            }

            ViewData["CarroFK"] = new SelectList(_context.Carros.OrderBy(c => c.Modelo), "Id", "Modelo", foto.CarroFK);

            return View(foto);
        }

        // GET: Fotografias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografias.FindAsync(id);
            if (fotografia == null)
            {
                return NotFound();
            }

            ViewData["CarroFK"] = new SelectList(_context.Carros.OrderBy(c => c.Modelo), "Id", "Modelo", fotografia.CarroFK);

            // guardar o ID do objeto enviado para o browser
            // através de uma variável de sessão
            HttpContext.Session.SetInt32("NumFotoEmEdicao", fotografia.Id);
            //  Session["idFoto"] = fotografias.Id;

            return View(fotografia);
        }

        // POST: Fotografias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fotografia,DataFoto,Local,CarroFK")] Fotografias foto)
        {
            if (id != foto.Id)
            {
                return NotFound();
            }

            // recuperar o ID do objeto enviado para o browser
            var numIdFoto = HttpContext.Session.GetInt32("NumFotoEmEdicao");

            // e compará-lo com o ID recebido
            // se forem iguais, continuamos
            // se forem diferentes, não fazemos a alteração

            if (numIdFoto == null || numIdFoto != foto.Id)
            {
                // se entro aqui, é pq houve problemas

                // redirecionar para a página de início
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiasExists(foto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroFK"] = new SelectList(_context.Carros, "Id", "Id", foto.CarroFK);
            return View(foto);
        }

        // GET: Fotografias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografias = await _context.Fotografias
                .Include(f => f.Carro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotografias == null)
            {
                return NotFound();
            }

            return View(fotografias);
        }

        // POST: Fotografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var fotografias = await _context.Fotografias.FindAsync(id);
            try
            {
                // Proteger a eliminação de uma foto
                _context.Fotografias.Remove(fotografias);
                await _context.SaveChangesAsync();

                // não esquecer, remover o ficheiro da Fotografia do disco rígido


            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        private bool FotografiasExists(int id)
        {
            return _context.Fotografias.Any(e => e.Id == id);
        }
    }
}
