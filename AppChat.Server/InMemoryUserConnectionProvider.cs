using AppChat.Domain.Abstractions;

namespace AppChat.Server
{
    public class InMemoryUserConnectionProvider : IUserConnectionProvider
    {
        private readonly Dictionary<string, string> _connections = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _userByConnections = new Dictionary<string, string>();

        public string this[string userName] => GetConnection(userName);

        public string GetConnection(string userName)
        {
            if(_connections.ContainsKey(userName))
                return _connections[userName];
            return null;
        }

        public string GetUser(string connection)
        {
            if (_userByConnections.ContainsKey(connection))
                return _connections[connection];
            return null;
        }

        public void UpdateConnection(string userName, string connectionId)
        {
            _userByConnections[connectionId] = userName;
            _connections[userName] = connectionId;
        }

        public string? RemoveConnection(string connectionId)
        {
            if (_userByConnections.ContainsKey(connectionId))
            {
                var userName = _userByConnections[connectionId];
                _connections.Remove(userName);
                _userByConnections.Remove(connectionId);
                return userName;
            }
            return null;
        }
    }
}
