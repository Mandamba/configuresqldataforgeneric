using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoArquitectural;
public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(string query, SqlParameter[] parameters);
    Task<IEnumerable<T>> ListAllAsync(string query);
    Task<int> CreateAsync(string query, T entity);
    Task<int> UpdateAsync(string query, T entity);
    Task<int> DeleteAsync(string query, SqlParameter[] parameters);
}
