using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string logPath = "validation.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Validate PDF and generate XML log
        using (Document doc = new Document(pdfPath))
        {
            // Validate against PDF/A-1B (any format works for the log)
            bool ok = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation completed: {(ok ? "passed" : "issues found")}");
        }

        if (!File.Exists(logPath))
        {
            Console.Error.WriteLine($"Validation log not found: {logPath}");
            return;
        }

        // Load XML validation report
        XDocument xml = XDocument.Load(logPath);

        // Assume each error is represented by an <Error> element with a Code attribute
        var errorElements = xml.Descendants("Error")
                               .Select(e => new
                               {
                                   Code = (string)e.Attribute("Code") ?? "UNKNOWN",
                                   Message = (string)e.Element("Message") ?? e.Value
                               })
                               .ToList();

        if (!errorElements.Any())
        {
            Console.WriteLine("No errors reported in the validation log.");
            return;
        }

        // Summarize by error code
        var byCode = errorElements
            .GroupBy(e => e.Code)
            .Select(g => new { Code = g.Key, Count = g.Count(), Sample = g.First().Message })
            .OrderByDescending(g => g.Count);

        Console.WriteLine("Error code summary:");
        foreach (var item in byCode)
        {
            Console.WriteLine($"Code: {item.Code}, Count: {item.Count}");
            Console.WriteLine($"Sample message: {item.Sample}\n");
        }

        // Most common violation messages (top 3)
        var topMessages = errorElements
            .GroupBy(e => e.Message)
            .Select(g => new { Message = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .Take(3);

        Console.WriteLine("Top violation messages:");
        foreach (var vm in topMessages)
        {
            Console.WriteLine($"\"{vm.Message}\" – {vm.Count} occurrence(s)");
        }
    }
}