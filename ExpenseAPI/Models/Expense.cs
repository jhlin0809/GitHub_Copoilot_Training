// 這是使用來代表一筆支出的 Entity
// 這個 Entity 有幾個欄位，分別是 Id, Date, Description, Amount, Title, Category
// Id 是一個數值 (int)，代表這筆支出的編號
// Date 是一個日期 (DateTime)，代表這筆支出的日期
// Description 是一個字串 (string)，代表這筆支出的說明
// Amount 是一個數值 (decimal)，代表這筆支出的金額
// Title 是一個字串 (string)，代表這筆支出的標題
// Category 是一個字串 (string)，代表這筆支出的類別

using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }
    }
}