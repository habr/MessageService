using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DatabaseInitializer : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public DatabaseInitializer(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using (var scope = _serviceProvider.CreateScope())
		{
			var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			using var connection = new NpgsqlConnection(connectionString);
			await connection.OpenAsync(cancellationToken);

			Console.WriteLine("Attempting to create the Messages table...");

			var createTableCommand = @"
            CREATE TABLE IF NOT EXISTS Messages (
                Id SERIAL PRIMARY KEY,
                Content VARCHAR(128) NOT NULL,
                CreatedAt TIMESTAMP NOT NULL,
                SequenceNumber INT NOT NULL
            );";

			using var command = new NpgsqlCommand(createTableCommand, connection);
			await command.ExecuteNonQueryAsync(cancellationToken);

			Console.WriteLine("Table creation complete.");
		}
	}


	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
