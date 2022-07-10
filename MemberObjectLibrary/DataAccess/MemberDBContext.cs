using MemberObjectLibrary.BussinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberObjectLibrary.DataAccess
{
    public class MemberDBContext : BaseDAL
    {
          private static MemberDBContext instance = null;
        private static readonly object instanceLock = new object();

        public static MemberDBContext Instance {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new MemberDBContext();
                    }
                    return instance;
                }
            } 
             
        }

        private MemberDBContext() { }

        //----------------------------------------------
        public IEnumerable<MemberObject> GetMemberList()
        {

             IDataReader dataReader = null;
             string SQLSelect = "Select Email , MemberName, Password, City, Country, isAdmin From MemberObject";
             var members = new List<MemberObject>();

            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    members.Add(new MemberObject
                    {
                        email = dataReader.GetString(0),
                        memberName = dataReader.GetString(1),
                        password = dataReader.GetString(2),
                        city = dataReader.GetString(3),
                        country = dataReader.GetString(4)
                    });
                }
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }

            return members;
        
        }
        //-----------------------
        public MemberObject GetMemberByEmail( string email)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelct = "SELECT Email , MemberName , Password , City , Country , isAdmin FROM MemberObject WHERE Email = @Email";
            try
            {
                var param = dataProvider.CreateParameter("@Email", 50, email, DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelct, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        email = dataReader.GetString(0),
                        memberName = dataReader.GetString(1),
                        password = dataReader.GetString(2),
                        city = dataReader.GetString(3),
                        country = dataReader.GetString(4)
                    };
                }            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }
        //-------------------------------------------
        public void AddNew(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByEmail(member.email);
                if (mem == null)
                {
                    string SQLInsert = "INSERT MemberObject Values(@Email,@memberName,@Password,@City ,@Country,@isAdmin)";
                    var parameter = new List<SqlParameter>();
                    parameter.Add(dataProvider.CreateParameter("@Email", 50, member.email, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@memberName", 50, member.memberName, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Password", 50, member.password, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@City", 50, member.city, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Country", 50, member.country, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@isAdmin", 4, member.isAdmin, DbType.Int32));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameter.ToArray());

                }
                else
                {
                    throw new Exception("The Car is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            }

        public void Update(MemberObject member)
        {
            try
            {
                MemberObject mem = GetMemberByEmail(member.email);
                if (mem == null)
                {
                    string SQLUpdate = "INSERT MemberObject set MemberName = @memberName,Password = @Password,City = @City ,Country = @Country,isAdmin = @isAdmin WHERE Email = @Email)";
                    var parameter = new List<SqlParameter>();
                    parameter.Add(dataProvider.CreateParameter("@Email", 50, member.email, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@memberName", 50, member.memberName, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Password", 50, member.password, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@City", 50, member.city, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@Country", 50, member.country, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@isAdmin", 4, member.isAdmin, DbType.Int32));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameter.ToArray());

                }
                else
                {
                    throw new Exception("The Car is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        //-------------------------------------------
        public void Remove(string Email)
        {
            try
            {
                MemberObject member = GetMemberByEmail(Email);
                if(member != null)
                {
                    string SQLDelete = "Delete MemberObject WHERE Email = @Email ";
                    var param = dataProvider.CreateParameter("@Email", 4, Email, DbType.String);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception(" The Car does not already exist");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }//end Remove

       /* public MemberObject SearchByName( string memberName)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelct = "SELECT Email , MemberName , Password , City , Country , isAdmin FROM MemberObject WHERE MemberName = @MemberName";
            try
            {
                var param = dataProvider.CreateParameter("@MemberName", 4, memberName, DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelct, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        email = dataReader.GetString(0),
                        memberName = dataReader.GetString(1),
                        password = dataReader.GetString(2),
                        city = dataReader.GetString(3),
                        country = dataReader.GetString(4)
                    };
                }            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }*/




        }//end class

    }// end namespace

