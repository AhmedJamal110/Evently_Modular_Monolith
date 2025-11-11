using Evently.Common.Application.Clock;

namespace Evently.Common.Infrastructure.Clock;
public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UTCNow => DateTime.UtcNow;
}
