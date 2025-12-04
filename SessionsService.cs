using System;
using System.Collections.Concurrent;

namespace SistemaFinanceiro
{
    public class SessionsService
    {
        // token => userId
        private readonly ConcurrentDictionary<string, int> _sessions = new();

        public string CreateSession(int userId)
        {
            var token = Guid.NewGuid().ToString("N");
            _sessions[token] = userId;
            return token;
        }

        public bool TryGetUserId(string token, out int userId)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                userId = 0;
                return false;
            }
            return _sessions.TryGetValue(token, out userId);
        }

        public void Remove(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return;
            _sessions.TryRemove(token, out _);
        }
    }
}
