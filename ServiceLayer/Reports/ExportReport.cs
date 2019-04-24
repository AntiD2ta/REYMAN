using BizLogic.Reports;

namespace ServiceLayer.Reports
{
    public class ExportReport
    {
        public byte[] ExportReport1(ReportOne report)
        {
            return new ExportReport1().Generate(report);
        }

        public byte[] ExportReport2(ReportTwo report)
        {
            return new ExportReport2().Generate(report);
        }

        public byte[] ExportReport4(ReportFour report)
        {
            return new ExportReport4().Generate(report);
        }

        public byte[] ExportReport5(ReportFive report)
        {
            return new ExportReport5().Generate(report);
        }
    }
}
