using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string logPath = "validation_report.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Validate PDF and write XML report
        using (Document doc = new Document(pdfPath))
        {
            // Any PdfFormat can be used; PDF/A-1B is a common choice
            doc.Validate(logPath, PdfFormat.PDF_A_1B);
        }

        if (!File.Exists(logPath))
        {
            Console.Error.WriteLine($"Validation log not created: {logPath}");
            return;
        }

        // Load the XML validation report
        XDocument report = XDocument.Load(logPath);

        // Heuristic: elements whose name contains "error" (case‑insensitive)
        var errorElements = report.Descendants()
            .Where(e => e.Name.LocalName.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0);

        var summary = errorElements
            .Select(e => (string)e.Attribute("code") ?? (string)e.Attribute("Code") ?? e.Value.Trim())
            .Where(c => !string.IsNullOrEmpty(c))
            .GroupBy(c => c)
            .Select(g => new { Code = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count);

        Console.WriteLine("Validation Summary (error code : occurrences):");
        foreach (var item in summary)
        {
            Console.WriteLine($"{item.Code} : {item.Count}");
        }
    }
}
