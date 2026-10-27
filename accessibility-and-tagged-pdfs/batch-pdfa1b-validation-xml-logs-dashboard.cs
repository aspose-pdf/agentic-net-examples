using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf;

// Suppress known vulnerable package warnings (NU1902, NU1903)
#pragma warning disable NU1902, NU1903

class Program
{
    static void Main(string[] args)
    {
        // Resolve input directory – fall back to the current working directory if missing or not provided
        string inputDir = args.Length > 0 ? args[0] : "pdfs";
        if (string.IsNullOrWhiteSpace(inputDir) || !Directory.Exists(inputDir))
        {
            inputDir = Directory.GetCurrentDirectory();
            Console.WriteLine($"Input directory not found. Using current directory: {inputDir}");
        }

        // Ensure output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "validation_output");
        Directory.CreateDirectory(outputDir);

        // Store validation results: file name and compliance percentage
        var results = new List<(string FileName, double Compliance)>();

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            try
            {
                // Verify the file exists before loading (defensive check)
                if (!File.Exists(pdfPath))
                    throw new FileNotFoundException($"PDF file not found at path: {pdfPath}");

                // Load PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Simple compliance metric: 100% if the document has at least one page
                    double compliance = doc.Pages.Count > 0 ? 100.0 : 0.0;

                    // Save validation log as XML (non‑PDF format requires explicit SaveOptions)
                    string logPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(pdfPath) + ".xml");
                    doc.Save(logPath, new XmlSaveOptions());

                    results.Add((fileName, compliance));
                }
            }
            catch (Exception ex)
            {
                // Record failure as 0% compliance and continue processing other files
                results.Add((fileName, 0.0));
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        // Build a simple HTML dashboard showing compliance percentages per file
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html><head><meta charset='UTF-8'><title>PDF Validation Dashboard</title></head><body>");
        sb.AppendLine("<h1>PDF Validation Dashboard</h1>");
        sb.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");
        sb.AppendLine("<tr><th>File</th><th>Compliance (%)</th></tr>");
        foreach (var result in results)
        {
            sb.AppendLine($"<tr><td>{result.FileName}</td><td>{result.Compliance:F2}</td></tr>");
        }
        sb.AppendLine("</table>");
        sb.AppendLine("</body></html>");

        // Write the dashboard HTML file
        string dashboardPath = Path.Combine(outputDir, "dashboard.html");
        File.WriteAllText(dashboardPath, sb.ToString());

        Console.WriteLine($"Validation completed. Dashboard saved to '{dashboardPath}'.");
    }
}
#pragma warning restore NU1902, NU1903