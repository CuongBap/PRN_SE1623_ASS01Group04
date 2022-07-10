using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberObjectLibrary.BussinessObject
{
    public class MemberObject
    {
        public string email { get; set; }
        public string memberName { get; set; }

        public string password { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int isAdmin { get; set; }
    }
}
