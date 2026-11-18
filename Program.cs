using System;

namespace SolidRefactoringLab
{
    // 1. OCP: Interface defining the formatting contract
    public interface IReportFormatter
    {
        void FormatReport();
    }

    // 2. OCP: Separate formatter implementations (open for extension, closed for modification)
    public class PdfFormatter : IReportFormatter
    {
        public void FormatReport()
        {
            Console.WriteLine("Formatting report as a PDF...");
        }
    }

    public class WordFormatter : IReportFormatter
    {
        public void FormatReport()
        {
            Console.WriteLine("Formatting report as a Word Document...");
        }
    }

    public class PlainTextFormatter : IReportFormatter
    {
        public void FormatReport()
        {
            Console.WriteLine("Formatting report as plain text...");
        }
    }

    // 3. SRP: Dedicated class solely responsible for saving files
    public class ReportSaver
    {
        public void SaveToFile(string filename)
        {
            Console.WriteLine($"Saving report data to {filename}...");
        }
    }

    // 4. SRP & DIP: ReportManager only coordinates report generation using an injected formatter
    public class ReportManager
    {
        private readonly IReportFormatter _formatter;
        public string ReportData { get; set; } = string.Empty;
        public ReportManager(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public void GenerateReport()
        {
            _formatter.FormatReport();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Choose the formatter (OCP)
            IReportFormatter formatter = new PdfFormatter();

            // 2. Pass it to the manager
            ReportManager manager = new ReportManager(formatter);
            manager.GenerateReport();

            // 3. Use the dedicated saver (SRP)
            ReportSaver saver = new ReportSaver();
            saver.SaveToFile("Q1_Report.pdf");
        }
    }
}