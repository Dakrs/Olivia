using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Olivia.DataAccess;
using Olivia.Controllers;
using Olivia.Models;

namespace Olivia.DataAccess
{
    public class InstructionDAO
    {
        public int Insert(Instruction instruction)
        {

            string sql = @"insert into dbo.Instruction  (Designation, Duration, Position, Id_Recipe) 
                                            values (@Designation, @Duration , @Position , @Id_Recipe);";

            InstructionData data = new InstructionData(instruction);

            return SqlDataAccess.SaveData(sql, data);
        }

        public List<Instruction> LoadInstructions()
        {
            string sql = @"select * from dbo.Instruction;";

            return SqlDataAccess.LoadData<Instruction>(sql, new Instruction());
        }

        public InstructionData GetInstruction(int recipe_id, int pos)
        {
            string sql = @"select * from dbo.Instruction where Id_Recipe=@Id_Recipe and Position=@Position;";

            return SqlDataAccess.LoadData<InstructionData>(sql, new InstructionData(new Instruction() { Id_Recipe = recipe_id, Position = pos})).Single<InstructionData>();
        }

        public List<Instruction> GetInstructions(int id_recipe)
        {
            string sql = @"select * from dbo.Instruction where Id_Recipe=@Id_Recipe;";

            return SqlDataAccess.LoadData<Instruction>(sql, new Instruction() { Id_Recipe = id_recipe});
        }

        public void DeleteInstruction(int recipe_id, int position)
        {
            string sql = @"delete from dbo.Instruction where Id_Recipe=@Id_Recipe and Position=@Position;";
            SqlDataAccess.SaveData(sql, new Instruction() { Id_Recipe = recipe_id, Position = position });
        }
    }
}