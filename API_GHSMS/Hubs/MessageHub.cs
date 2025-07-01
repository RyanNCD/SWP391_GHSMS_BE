//using Microsoft.AspNetCore.SignalR;

//namespace API_GHSMS.Hubs
//{
//    public class MessageHub : Hub
//    {
//        private readonly GroupRepository _groupRepository;
//    private readonly ConnectionRepository _connectionRepository;
//    private readonly UserRepository _userRepository;
//    private readonly UserMessageRepository _userMessageRepository;
//    private readonly LastUserMessageRepository _lastUserMessageRepository;
    
//    public MessageHub(GroupRepository groupRepository, ConnectionRepository connectionRepository, 
//        UserRepository userRepository, UserMessageRepository userMessageRepository, LastUserMessageRepository lastUserMessageRepository)
//    {
//        _groupRepository = groupRepository;
//        _connectionRepository = connectionRepository;
//        _userRepository = userRepository;
//        _userMessageRepository = userMessageRepository;
//        _lastUserMessageRepository = lastUserMessageRepository;
//    }
    
//    public override async Task OnConnectedAsync()
//    {
//        var httpContext = Context.GetHttpContext();
//        var otherUser = httpContext.Request.Query["user"].ToString();
//        if (otherUser.Equals("undefined")) throw new HubException("User not found");
//        var groupName = GetGroupName(Context.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value, otherUser);
//        // _logger.Information($"Group name: {groupName}");
//        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);S
//        var group = await AddToGroup(groupName);
//        var messages = await GetMessageThread(Context.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value, otherUser);
//        await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
//    }

//    private async Task<IEnumerable<UserMessageDto>> GetMessageThread(string currentEmail, string receiverEmail)
//    {
//        var messages = await _userMessageRepository.GetUserMessageBySenderAndReceiverAsync(currentEmail, receiverEmail);
//        var response = new List<UserMessageDto>();
//        if (messages != null)
//        {
//            foreach (var message in messages)
//            {
//                response.Add(new UserMessageDto
//                {
//                    MessageId = message.MessageId,
//                    Message = message.Message,
//                    SenderEmail = message.SenderEmail,
//                    ReceiverEmail = message.ReceiverEmail,
//                    SentAt = message.SentAt
//                });
//            }
//        }

//        return response;
//    }
//    public async Task SendMessage(CreateMessageDto messageDto)
//    {
//        var email = Context.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
//        if(email == messageDto.ReceiverEmail)
//            throw new HubException("You cannot send a message to yourself");
//        var sender = await _userRepository.GetUserByEmailAsync(email);
//        var receiver = await _userRepository.GetUserByEmailAsync(messageDto.ReceiverEmail);
        
//        if (sender == null || receiver == null)
//            throw new HubException("Sender or receiver not found");

//        var userMessage = new UserMessage()
//        {
//            MessageId = Guid.NewGuid(),
//            Message = messageDto.Message,
//            SenderEmail = sender.Email,
//            ReceiverEmail = receiver.Email,
//            SentAt = DateTime.UtcNow
//        };
//        await _userMessageRepository.CreateAsync(userMessage);
//        await UpdateLastMessageChat(userMessage, sender, receiver);
//        var userMessageDto = new UserMessageDto
//        {
//            MessageId = userMessage.MessageId,
//            Message = userMessage.Message,
//            SenderEmail = userMessage.SenderEmail,
//            ReceiverEmail = userMessage.ReceiverEmail,
//            SentAt = userMessage.SentAt
//        };
//        await Clients.Group(GetGroupName(sender.Email, receiver.Email)).SendAsync("ReceiveMessage", userMessageDto);
//    }

//    private async Task UpdateLastMessageChat(UserMessage userMessage, User sender, User receiver)
//    {
//        var lastMessageFromDb = await _lastUserMessageRepository.GetLastUserMessageBySenderAndReceiverAsync(sender.Email, receiver.Email);
//        if (lastMessageFromDb != null)
//        {
//            lastMessageFromDb.Message = userMessage.Message;
//            lastMessageFromDb.MessageLastDate = DateTime.UtcNow;
//            await _lastUserMessageRepository.UpdateAsync(lastMessageFromDb);
//        }
//        else
//        {
//            var groupName = GetGroupName(sender.Email, receiver.Email);
//            var lastUserMessage = new LastUserMessage
//            {
//                LastUserMessageId = Guid.NewGuid(),
//                SenderEmail = sender.Email,
//                ReceiverEmail = receiver.Email,
//                Message = userMessage.Message,
//                MessageLastDate = DateTime.UtcNow,
//                GroupName = groupName,
//                CreatedAt = DateTime.UtcNow,
//                SenderAvatarUrl = sender.Avatar,
//                ReceiverAvatarUrl = receiver.Avatar
//            };
//            await _lastUserMessageRepository.CreateAsync(lastUserMessage);
//        }
//    }
    
//    public override async Task OnDisconnectedAsync(Exception? exception)
//    {
//        var group = await RemoveFromMessageGroup();
//        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.Name);
//        await base.OnDisconnectedAsync(exception);
//    }
    
//    private string GetGroupName(string caller, string other)
//    {
//        var stringCompare = string.CompareOrdinal(caller, other) < 0;
//        return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
//    }
//    private async Task<Group> AddToGroup(string groupName)
//    {
//        // _logger.Information($"Group Name: {groupName}");
//        try
//        {
//            var group = await _groupRepository.GetGroupByNameAsync(groupName);
//            var connection = new Connection
//            {
//                ConnectionId = Context.ConnectionId,
//                Email = Context.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value,
//                GroupName = groupName
//            };
//            if (group == null)
//            {
//                group = new Group
//                {
//                    Name = groupName
//                };
//                await _groupRepository.CreateAsync(group);
//            }

//            await _connectionRepository.CreateAsync(connection);

//            return group;
//        }
//        catch (Exception e)
//        {
//            throw new HubException("Failed to join group", e);
//        }
        
//    }
//    private async Task<Group> RemoveFromMessageGroup()
//    {
//        var group = await _groupRepository.GetGroupByConnectionIdAsync(Context.ConnectionId);
//        var connection = await _connectionRepository.GetConnectionByIdAsync(Context.ConnectionId);
//        var isSuccess = await _connectionRepository.RemoveAsync(connection);
        
//        if(isSuccess) return group;

//        throw new HubException("Fail to remove from group");
//    }
//    }
//}
