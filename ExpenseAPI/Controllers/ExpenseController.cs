// 這個 API Controller 會使用 Expense.cs 這個 Model 來處理支出的資料
// API 路徑是 /api/expense
// 這個 Controller 有以下幾個方法：
// 1. GET /api/expense
//    這個方法會回傳所有的支出資料
// 2. GET /api/expense/{id}
//    這個方法會回傳指定 id 的支出資料
// 3. POST /api/expense
//    這個方法會新增一筆支出資料
// 4. PUT /api/expense/{id}
//    這個方法會更新指定 id 的支出資料
// 5. DELETE /api/expense/{id}
//    這個方法會刪除指定 id 的支出資料

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseAPI.Models;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;
        }

        // GET /api/expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET /api/expense/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // POST /api/expense
        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT /api/expense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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

        // DELETE /api/expense/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}