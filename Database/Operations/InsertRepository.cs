using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineTestModels;

namespace Database.Operations
{
    public class InsertRepository
    {
        public string  AddUser(UserModelModel user)
        {
            var FinalCode = getFinalCode(user.InitCode);
            using (var content = new MachineTestEntities())
            {
                Users usermod = new Users()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    InitCode = user.InitCode,
                    FinalCode = FinalCode,
                    Password = user.Password
                };

                content.Users.Add(usermod);
                content.SaveChanges();
            }
               
            return FinalCode;
        }

        private string getFinalCode(string initCode)
        {
            string FinalCode = "";
            using (var content = new MachineTestEntities())
            {
                var result = content.Users.Where(x => x.InitCode == initCode).
                    Select(x => x.InitCode == initCode).ToList();
                if (result.Count >= 0 && result.Count<10)
                {
                    FinalCode = (initCode + "000" + (Convert.ToInt32(result.Count) + 1)).ToString();
                }
               else
                {
                    FinalCode = (initCode + "00" + (Convert.ToInt32(result.Count) + 1)).ToString();
                }
            }
            
            return FinalCode;
        }

        public object LoginUser(string userId, string password)
        {
            string message = "";
            var res = new object[2];
            using (var context = new MachineTestEntities())
            {
                var list = context.Users.Where(x => x.FinalCode == userId && x.Password == password).
                    Select(x=> new UserModelModel()
                    {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        FinalCode = x.FinalCode
                    }
                    ).ToList();

                if(list.Count > 0)
                {
                    message = "success";
                    res[0] = list;
                }
                else
                {
                    message = "Ivalid Credentials";
                }
            }
            res[1] = message;
           
            return res;
        }

        public string AddDataRec(DatarecordModelModel datarec)
        {
            using (var content = new MachineTestEntities())
            {
                DataRecords dataRecords = new DataRecords() { 
                    Code = datarec.Code,
                    Type = datarec.Type,
                    Total = datarec.Total,
                    Hours = datarec.Hours,
                    Rate = datarec.Rate
                };

                content.DataRecords.Add(dataRecords);
                content.SaveChanges();
            }
            return "success";
        }
    }
}
