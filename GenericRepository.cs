using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using SimulacaoArquitectural;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
{
    private readonly string _connectionString;

    protected GenericRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected abstract T Map(SqlDataReader reader);
    public async Task<IEnumerable<T>> ListAllAsync(string query)
    {
        var items = new List<T>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            items.Add(Map(reader));
        }
        return items;
    }

    public async Task<int> CreateAsync(string query, T entity)
    {
        var parameters = GetParameters(entity);
        return await ExecuteNonQueryAsync(query, parameters);
    }

    public async Task<int> UpdateAsync(string query, T entity)
    {
        var parameters = GetParameters(entity);
        return await ExecuteNonQueryAsync(query, parameters);
    }

    private async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }
    private SqlParameter[] GetParameters(T entity)
    {
        var properties = typeof(T).GetProperties();
        var parameters = new List<SqlParameter>();

        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
            parameters.Add(new SqlParameter($"@{property.Name}", value ?? DBNull.Value));
        }

        return parameters.ToArray();
    }

    public async Task<T> GetByIdAsync(string query, SqlParameter[] parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddRange(parameters);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        return await reader.ReadAsync() ? Map(reader) : null;
    }

    public async Task<int> DeleteAsync(string query, SqlParameter[] parameters)
    {
        return await ExecuteNonQueryAsync(query, parameters);
    }
}
