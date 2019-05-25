using System;
namespace Olivia.Models
{
    public class Instruction
    {
        public int Id_Recipe { get; set; }
        public int Position { get; set; }
        public string Designation { get; set; }
        public int Duration { get; set; }
    }
}
