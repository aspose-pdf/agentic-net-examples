using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class ValidationReportParser
{
    static void Main()
    {
        // Paths for the input PDF and the validation log (XML)
        const string pdfPath = "input.pdf";
        const string logPath = "validation_report.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Validate the PDF and generate an XML log file
        bool isValid;
        using (Document doc = new Document(pdfPath))
        {
            // Validate against PDF/A-1B (any PdfFormat can be used)
            isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
        }

        Console.WriteLine(isValid
            ? "Document passed validation."
            : "Document failed validation – see report for details.");

        if (!File.Exists(logPath))
        {
            Console.Error.WriteLine($"Validation log not created: {logPath}");
            return;
        }

        // Load the XML validation report
        XDocument report = XDocument.Load(logPath);

        // Example XML structure:
        // <ValidationReport>
        //   <Error Code="E001" Message="..." />
        //   <Error Code="E002" Message="..." />
        // </ValidationReport>

        // Extract all error elements and their codes
        var errorElements = report.Descendants("Error")
                                  .Select(e => (string)e.Attribute("Code"))
                                  .Where(code => !string.IsNullOrEmpty(code))
                                  .ToList();

        if (!errorElements.Any())
        {
            Console.WriteLine("No errors found in the validation report.");
            return;
        }

        // Group by error code and count occurrences
        var errorSummary = errorElements
            .GroupBy(code => code)
            .Select(g => new { Code = g.Key, Count = g.Count() })
            .OrderByDescending(item => item.Count)
            .ToList();

        // Output the summary
        Console.WriteLine("\nError Code Summary:");
        foreach (var entry in errorSummary)
        {
            Console.WriteLine($"Code: {entry.Code} – Occurrences: {entry.Count}");
        }

        // Identify the most common violation(s)
        int maxCount = errorSummary.First().Count;
        var mostCommon = errorSummary.Where(e => e.Count == maxCount).Select(e => e.Code).ToArray();

        Console.WriteLine("\nMost Common Violation(s):");
        foreach (string code in mostCommon)
        {
            Console.WriteLine($"- {code}");
        }
    }
}