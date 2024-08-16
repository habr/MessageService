using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MessageService.Models;
using Newtonsoft.Json;

public class MessageWebSocketHandler
{
	private static readonly List<WebSocket> ConnectedSockets = new();

	public async Task HandleWebSocketAsync(HttpContext context)
	{
		if (context.WebSockets.IsWebSocketRequest)
		{
			var webSocket = await context.WebSockets.AcceptWebSocketAsync();
			ConnectedSockets.Add(webSocket);

			await ReceiveMessagesAsync(webSocket);
		}
		else
		{
			context.Response.StatusCode = 400;
		}
	}

	private async Task ReceiveMessagesAsync(WebSocket webSocket)
	{
		var buffer = new byte[1024 * 4];
		while (webSocket.State == WebSocketState.Open)
		{
			var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

			if (result.MessageType == WebSocketMessageType.Text)
			{
				// Получаем сообщение и отсылаем его всем подключенным клиентам
				var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
				await BroadcastMessageAsync(receivedMessage);
			}
			else if (result.MessageType == WebSocketMessageType.Close)
			{
				await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the client", CancellationToken.None);
				ConnectedSockets.Remove(webSocket);
			}
		}
	}

	public async Task BroadcastMessageAsync(string message)
	{
		foreach (var socket in ConnectedSockets)
		{
			if (socket.State == WebSocketState.Open)
			{
				var messageBuffer = Encoding.UTF8.GetBytes(message);
				await socket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
			}
		}
	}

	public async Task BroadcastMessageAsync(Message message)
	{
		var messageJson = JsonConvert.SerializeObject(message);
		await BroadcastMessageAsync(messageJson);
	}
}
