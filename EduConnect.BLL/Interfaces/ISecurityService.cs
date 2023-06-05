using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.BLL.Interfaces
{
    public interface ISecurityService
    {
        public string EncryptPassword(string password);
        public bool VerifyPassword(string password,string hashedPassword);
    }
}
