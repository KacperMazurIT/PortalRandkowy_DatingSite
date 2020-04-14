﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    // http://localhost:5000/
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await  _context.Values.ToListAsync();
            return Ok(values);
        }

        // GET values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        // POST values
        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        // PUT values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id, [FromBody] Value value)
        {
            var data = await _context.Values.FindAsync(id);
            data.Name = value.Name;
            _context.Values.Update(data);
            await _context.SaveChangesAsync();
             return Ok(data);
        }

        // DELETE values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var data = await _context.Values.FindAsync(id);

            if(data == null)
                return NoContent();

            _context.Values.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}
