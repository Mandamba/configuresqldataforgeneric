using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoArquitectural;
public interface IUserRepository : IGenericRepository<User>
{
    Task<User> ChangePassword(User user);
}
