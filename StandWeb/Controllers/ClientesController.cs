﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StandWeb.Data;
using StandWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace StandWeb.Controllers
{


    [Authorize]
    public class ClientesController : Controller
    {
        /// <summary>
        /// referência à base de dados
        /// </summary>
        private readonly GostosDB _context;

        /// <summary>
        /// objeto que sabe interagir com os dados do utilizador q se autentica
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientesController(
           GostosDB context,
           UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        /// <summary>
        /// Método para apresentar os dados dos Clientes a autorizar
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //autorizar apenas gestores a aceder a esta função
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> ListaClientesPorAutorizar()
        {
            // quais os Clientes ainda não autorizados a aceder ao Sistema?
            // lista com os utilizadores bloqueados
            var listaDeUtilizadores = _userManager.Users.Where(u => u.LockoutEnd > DateTime.Now);
            // lista com os dados dos Clientes
            var listaClientes = _context.Clientes.Where(c => listaDeUtilizadores.Select(u => u.Id).Contains(c.UserName));
            // Enviar os dados para a View
            return View(await listaClientes.ToListAsync());
            }

        /// <summary>
        /// método que recebe os dados dos utilizadores a desbloquear
        /// </summary>
        /// <param name="utilizadores">lista desses utilizadores</param>
        /// <returns></returns>
        [HttpPost]
        //autorizar apenas gestores a aceder a esta função 
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> ListaClientesPorAutorizar(string[] utilizadores)
        {
            // será que algum utilizador foi selecionado?
            if (utilizadores.Count() != 0)
            {
                // há users selecionados
                // para cada um, vamos desbloqueá-los
                foreach (string u in utilizadores)
                {
                    try
                    {
                        // procurar o 'utilizador' na tabela dos Users
                        var user = await _userManager.FindByIdAsync(u);
                        // desbloquear o utilizador
                        await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddDays(-1));
                        // como não se pediu ao User para validar o seu email
                        // é preciso aqui validar esse email
                        string codigo = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _userManager.ConfirmEmailAsync(user, codigo);

                        // eventualmente, poderá ser enviado um email para o utilizador a avisar que 
                        // a sua conta foi desbloqueada
                    }
                    catch (Exception)
                    {
                        // deveria haver aqui uma mensagem de erro para o utilizador,
                        // se assim o entender
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Morada,CodPostal,Telemovel,Email")] Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.Id))
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
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criadores = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criadores == null)
            {
                return NotFound();
            }

            return View(criadores);
        }

        // POST: Criadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
