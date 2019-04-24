using BizDbAccess.GenericInterfaces;
using BizLogic.Reports;
using DataLayer.EfCode;
using System.Collections.Generic;

namespace ServiceLayer.Reports
{
    public class GenerateReport
    {
        private readonly EfCoreContext _context;

        public GenerateReport(IUnitOfWork unitOfWork)
        {
            _context = (EfCoreContext)unitOfWork;
        }

        public ReportOne GenerateReport1(int year, string tipoPlan, IEnumerable<string> uos, IEnumerable<string> inmuebles)
        {
            GenerateReport1 report = new GenerateReport1(_context);
            return report.GenerateReport(year, tipoPlan, uos, inmuebles);
        }

        public ReportTwo GenerateReport2(int year, IEnumerable<string> uos)
        {
            GenerateReport2 report = new GenerateReport2(_context);
            return report.GenerateReport(year, uos);
        }

        public ReportFour GenerateReport4(int year, IEnumerable<string> uos)
        {
            GenerateReport4 report = new GenerateReport4(_context);
            return report.GenerateReport(year, uos);
        }

        public ReportFive GenerateReport5(int year, IEnumerable<string> uos)
        {
            GenerateReport5 report = new GenerateReport5(_context);
            return report.GenerateReport(year, uos).Result;
        }
    }
}
