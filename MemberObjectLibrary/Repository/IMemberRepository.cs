using MemberObjectLibrary.BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberObjectLibrary.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMemberObjects();
        MemberObject GetMemByEmail(string email);
        void InsertMember(MemberObject member);
        void UpdateMember(MemberObject member);
        void DeleteMember(string email);

        
    }
}
