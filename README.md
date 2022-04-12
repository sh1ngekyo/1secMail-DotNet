# 1secMail-DotNet
DotNet API for creating and managing temporary emails based on [1secMail](https://www.1secmail.com).

Using:
```csharp
using OneSecEmailDotNet.Core;
```

Create instance of EmailService:
```csharp
using (var es = new EmailService(/*optional: proxy*/))
{
//do your work
}
```

Create new email address:
```csharp
using (var es = new EmailService(/*optional: proxy*/))
{
	//Create 1 mailbox
	var mailbox = await es.CreateAsync();
	//Create 5 mailboxes
	var mailboxes = await es.CreateAsync(5);
}
```

Get message by ID:
```csharp
await es.GetMessageByIdAsync(mailbox, 123);
```

Download attachments:
```js
await es.DownloadAttachmentAsync(email, message.Id, attachment.FileName);
```

Example update:
```csharp
while (!await es.ContainsNewMessagesAsync(mailbox))
{
	//check every 20 sec
	await Task.Delay(1000 * 20);
}
//email auto update 
await es.UpdateEmailAsync(mailbox);
```

Or do it manualy with:
```csharp
GetNewMessagesId(...);
GetMessageByIdAsync(...);
```

Dispose after done:
```csharp
es.Dispose();
```