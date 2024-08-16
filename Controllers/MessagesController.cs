using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessageService.Data;
using MessageService.Models;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
	private readonly IMessageRepository _messageRepository;
	private readonly MessageWebSocketHandler _webSocketHandler;
	public MessagesController(IMessageRepository messageRepository, MessageWebSocketHandler webSocketHandler)
	{
		_messageRepository = messageRepository;
		_webSocketHandler = webSocketHandler;
	}

	[HttpPost]
	public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
	{
		var message = new Message
		{
			Content = messageDto.Content,
			SequenceNumber = messageDto.SequenceNumber,
			CreatedAt = DateTime.UtcNow
		};

		await _messageRepository.AddMessageAsync(message);

		// Трансляция сообщения всем подключенным WebSocket клиентам
		await _webSocketHandler.BroadcastMessageAsync(message);

		return Ok(message);
	}

	[HttpGet]
	public async Task<IActionResult> GetMessages([FromQuery] DateTime start, [FromQuery] DateTime end)
	{
		var messages = await _messageRepository.GetMessagesAsync(start, end);
		return Ok(messages);
	}
}

public class MessageDto
{
	public string Content { get; set; }
	public int SequenceNumber { get; set; }
}
