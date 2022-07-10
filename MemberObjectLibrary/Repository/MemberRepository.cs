using MemberObjectLibrary.BussinessObject;
using MemberObjectLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberObjectLibrary.Repository
{
    public class MemberRepository : IMemberRepository
    {

        public MemberObject GetMemByEmail(string email) => MemberDBContext.Instance.GetMemberByEmail(email);
        

        public IEnumerable<MemberObject> GetMemberObjects() => MemberDBContext.Instance.GetMemberList();



        public void InsertMember(MemberObject member) => MemberDBContext.Instance.AddNew(member);

        public void DeleteMember(string email) => MemberDBContext.Instance.Remove(email);


        public void UpdateMember(MemberObject member) => MemberDBContext.Instance.Update(member);

       
       
    }
}
