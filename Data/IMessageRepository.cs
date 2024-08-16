namespace MessageService.Data
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using MessageService.Models;

	public interface IMessageRepository
	{
		Task AddMessageAsync(Message message);
		Task<IEnumerable<Message>> GetMessagesAsync(DateTime start, DateTime end);
	}

}
