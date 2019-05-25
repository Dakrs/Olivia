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

            return SqlDataAccess.LoadData<Instruction>(sql);
        }

        public Instruction FindById(int id)
        {
            string sql = @"select * from dbo.Instruction where Id_Instruction='" + id + "';";

            return SqlDataAccess.LoadData<Instruction>(sql).Single<Instruction>();
        }

        public Instruction FindByName(string name)
        {
            string sql = @"select * from dbo.Instruction where Name='" + name + "';";

            return SqlDataAccess.LoadData<Instruction>(sql).Single<Instruction>();
        }

        public InstructionData GetInstruction(int recipe_id, int pos)
        {
            string sql = @"select * from dbo.Instruction where Id_Recipe='" + recipe_id + "' and Position='" + pos + "';";

            return SqlDataAccess.LoadData<InstructionData>(sql).Single<InstructionData>();
        }

        public List<Instruction> GetInstructions(int id_recipe)
        {
            string sql = @"select * from dbo.Instruction where Id_Recipe='" + id_recipe + "';";

            return SqlDataAccess.LoadData<Instruction>(sql);
        }

        public void DeleteInstruction(int recipe_id, int position)
        {
            string sql = @"delete from dbo.Instruction where Id_Recipe='" + recipe_id + "' and Position='" + position + "';";
            SqlDataAccess.SaveData(sql, sql);
        }
    }
}