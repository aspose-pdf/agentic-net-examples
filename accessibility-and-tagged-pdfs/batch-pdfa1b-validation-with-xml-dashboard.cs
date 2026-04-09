using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to validate
        const string inputFolder = "InputPdfs";
        // Folder where individual validation logs will be stored
        const string logFolder = "ValidationLogs";
        // Path for the summary dashboard XML
        const string dashboardPath = "ValidationDashboard.xml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(logFolder);

        // Collect results for each file
        var results = new List<(string FileName, bool IsValid)>();

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string logPath = Path.Combine(logFolder, Path.GetFileNameWithoutExtension(pdfPath) + "_log.xml");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Validate against PDF/A-1b format and write XML log
                    bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                    results.Add((fileName, isValid));
                }

                Console.WriteLine($"Validated: {fileName} (Log: {logPath})");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
                results.Add((fileName, false));
            }
        }

        // Compute compliance percentage
        int total = results.Count;
        int compliant = 0;
        foreach (var r in results)
            if (r.IsValid) compliant++;

        double compliancePercent = total > 0 ? (double)compliant / total * 100.0 : 0.0;

        // Output simple console dashboard
        Console.WriteLine("\n=== Validation Dashboard ===");
        Console.WriteLine($"Total files   : {total}");
        Console.WriteLine($"Compliant     : {compliant}");
        Console.WriteLine($"Compliance %  : {compliancePercent:F2}%");
        Console.WriteLine("\nPer‑file results:");
        foreach (var r in results)
            Console.WriteLine($"{r.FileName}: {(r.IsValid ? "PASS" : "FAIL")}");

        // Create an XML dashboard summarizing the results
        var dashboardXml =
            new XElement("ValidationDashboard",
                new XAttribute("TotalFiles", total),
                new XAttribute("CompliantFiles", compliant),
                new XAttribute("CompliancePercentage", compliancePercent.ToString("F2")),
                new XElement("Files",
                    new List<XElement>(results.ConvertAll(r =>
                        new XElement("File",
                            new XAttribute("Name", r.FileName),
                            new XAttribute("Result", r.IsValid ? "Pass" : "Fail"))))));

        // Save the dashboard XML
        dashboardXml.Save(dashboardPath);
        Console.WriteLine($"\nDashboard XML saved to '{dashboardPath}'.");
    }
}