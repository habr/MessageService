using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using MessageService.Models;
using MessageService.Data;

public class MessageRepository : IMessageRepository
{
	private readonly string _connectionString;

	public MessageRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection");
	}

	private IDbConnection CreateConnection()
	{
		return new NpgsqlConnection(_connectionString);
	}

	public async Task AddMessageAsync(Message message)
	{
		const string query = @"
            INSERT INTO Messages (Content, CreatedAt, SequenceNumber) 
            VALUES (@Content, @CreatedAt, @SequenceNumber)";

		using var connection = CreateConnection();
		await connection.ExecuteAsync(query, message);
	}

	public async Task<IEnumerable<Message>> GetMessagesAsync(DateTime start, DateTime end)
	{
		const string query = @"
            SELECT * FROM Messages 
            WHERE CreatedAt BETWEEN @Start AND @End";

		using var connection = CreateConnection();
		return await connection.QueryAsync<Message>(query, new { Start = start, End = end });
	}
}

