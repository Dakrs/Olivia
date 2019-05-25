using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Olivia.DataAccess;

namespace Olivia.Models
{
    public class InstructionData
    {

        public int Id_Recipe { get; set; }
        public int Position { get; set; }
        public string Designation { get; set; }
        public int Duration { get; set; }


        public InstructionData(Instruction instruction)
        {
            Id_Recipe = instruction.Id_Recipe;
            Position = instruction.Position;
            Designation = instruction.Designation;
            Duration = instruction.Duration;
        }
    }
}
