using Microsoft.EntityFrameworkCore;
using ReportCardz.Models;

namespace ReportCardz.Persistence;

public class ReportCardsRepository: IReportCardsRepository
{
    private readonly ReportCardsDbContext _context;
    public ReportCardsRepository(ReportCardsDbContext context)
    {
        _context = context;
    }

    public IQueryable<Child> GetChildsAsync()
    {
        return _context.Children;
    }
}

public interface IReportCardsRepository
{
    IQueryable<Child> GetChildsAsync();
}