using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoArquitectural;
public class UserRepository : GenericRepository<User>,IUserRepository 
{
    public UserRepository(string connectionString) : base(connectionString)
    {}

    public Task<User> ChangePassword(User user)
    {
        throw new NotImplementedException();
    }

    protected override User Map(SqlDataReader reader)
    {
        return new User
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Email = reader.GetString(reader.GetOrdinal("Email"))
        };
    }
}
