﻿using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly Contexto _context;

        public TiposController(Contexto context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            return await _context.Tipos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo>> GetTipo(int id)
        {
            var tipo = await _context.Tipos.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }
            return tipo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTipo(int id, Tipo tipo)
        {
            if (id != tipo.TipoId)
            {
                return BadRequest();
            }
            _context.Entry(tipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExists(id))
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

        private bool TipoExists(int id)
        {
            return _context.Tipos.Any(e => e.TipoId == id);
        }

        [HttpPost]
        public async Task<ActionResult<Tipo>> PostTipo(Tipo tipo)
        {
            _context.Tipos.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTipo", new { id = tipo }, tipo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tipo>> DeleteTipo(int id)
        {
            var tipo = await _context.Tipos.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            _context.Tipos.Remove(tipo);
            await _context.SaveChangesAsync();
            return tipo;
        }
    }
}
