using System;
namespace Olivia.Models
{
    public class Instruction
    {
        public int Id_Recipe { get; set; }
        public int Position { get; set; }
        public string Designation { get; set; }
    
        public Instruction() { }

        public Instruction(Instruction ins)
        {
            Id_Recipe = ins.Id_Recipe;
            Position = ins.Position;
            Designation = ins.Designation;
        }
    }

}
