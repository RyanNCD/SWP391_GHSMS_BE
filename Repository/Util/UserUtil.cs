using System.Security.Claims;

namespace Repository.Util
{
    public static class UserUtil
    {
        private static readonly object _lock = new object();
        private static long _lastGenerated = DateTime.UtcNow.Ticks;

        public static Guid GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out Guid userId))
                return userId;

            throw new Exception("UserId không hợp lệ hoặc chưa đăng nhập.");
        }

        public static long GenerateUniqueLong()
        {
            lock (_lock)
            {
                long timestamp = DateTime.UtcNow.Ticks;

                // đảm bảo không bị trùng nếu gọi liên tiếp rất nhanh
                if (timestamp <= _lastGenerated)
                {
                    timestamp = _lastGenerated + 1;
                }

                _lastGenerated = timestamp;
                return timestamp;
            }
        }

        public static long GenerateOrderCode()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

    }
}
