using DAT154Oblig4.Application.Common.Interfaces;

namespace DAT154Oblig4.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
