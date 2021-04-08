using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Test.Helpers;
using Test.Entities;
using Test.Models.Roulette;
using Test.Models.Bet;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Test.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly DataContext _context;
        private IMapper _mapper;

        public RouletteController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;            
        }

        // GET: api/Roulette
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> Getroulettes()
        {
            return await _context.roulettes.ToListAsync();
        }

        // GET: api/Roulette/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roulette>> GetRoulette(long id)
        {
            var roulette = await _context.roulettes.FindAsync(id);

            if (roulette == null)
            {
                return NotFound();
            }

            return roulette;
        }

        // PUT: api/Roulette/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("close/{id}")]
        public async Task<IActionResult> PutRoulette(long id, Roulette roulette)
        {
            if (id != roulette.Id)
            {
                return BadRequest();
            }

            _context.Entry(roulette).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouletteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseRoulette(long id)
        {
            var roulette = await _context.roulettes.FindAsync(id);
            if (roulette == null)
            {
                return NotFound();
            }
            roulette.Open = false;
            await _context.SaveChangesAsync();

            return Ok(new {message = "Ruleta close"});
        }

        [HttpPut("open/{id}")]
        public async Task<IActionResult> OpenRoulette(long id)
        {
            var roulette = await _context.roulettes.FindAsync(id);
            if (roulette == null)
            {
                return NotFound();
            }
            roulette.Open = true;
            await _context.SaveChangesAsync();

            return Ok(new {message = "Ruleta abierta"});
        }

        [HttpPut("bet/{id}")]
        public async Task<ActionResult<Roulette>> PostBet(long id, [FromBody]CreateBetModel model)
        {
            var roulette = await _context.roulettes.FindAsync(id);
            if (roulette == null)
            {
                return NotFound();
            }
            if(roulette.Open == true){                
                var bets = _context.bets.Where(c => c.RouletteId == id);
                int sum = 0;
                foreach (var bet in bets ){
                    sum += bet.Money;
                }
                if(sum + model.Money <= 10000){
                    var idUser = HttpContext.User.Identity.Name;
                    var user = await _context.users.FindAsync(int.Parse(idUser));
                    if(user.Money >= model.Money){
                        Bet bet = new Bet();
                        bet.RouletteId = id;
                        bet.UserId = int.Parse(idUser);
                        if(model.Color == "Rojo"){
                            bet.Color =  TypeColor.Red;
                        }else if(model.Color == "Negro"){
                            bet.Color =  TypeColor.Black;
                        }
                        bet.Number = model.Number;
                        bet.Money = model.Money;
                        _context.bets.Add(bet);
                        await _context.SaveChangesAsync();
                    }else{
                        return BadRequest(new {message = "No tienes suficiente dinero para realizar esta apuesta"});
                    }
                }else{
                    return BadRequest(new {message = "La ruleta llego a su maximo cantidad de dinero apostado"});
                }
            }else{
                return BadRequest(new {message = "Ruleta cerrada"});
            }

            return Ok(new {message = "Apuesta realizada con exito"});
        }

        // POST: api/Roulette
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roulette>> PostRoulette([FromBody]CreateModel model)
        {
            var roulette = _mapper.Map<Roulette>(model);
            if (_context.roulettes.Any(x => x.Name == roulette.Name))
                throw new AppException("Nombre \"" + roulette.Name + "\" no se encuentra disponible");

            _context.roulettes.Add(roulette);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoulette", new { id = roulette.Id }, roulette);
        }

        // DELETE: api/Roulette/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoulette(long id)
        {
            var roulette = await _context.roulettes.FindAsync(id);
            if (roulette == null)
            {
                return NotFound();
            }

            _context.roulettes.Remove(roulette);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouletteExists(long id)
        {
            return _context.roulettes.Any(e => e.Id == id);
        }
    }
}
