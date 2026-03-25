using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing PDFs (default "pdfs")
        string inputFolder = args.Length > 0 ? args[0] : "pdfs";
        // Folder where XML validation logs will be stored (default "validation-logs")
        string logFolder = args.Length > 1 ? args[1] : "validation-logs";

        // Ensure the log folder exists
        Directory.CreateDirectory(logFolder);

        // Verify the input folder exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please provide a valid folder containing PDF files.");
            return;
        }

        var results = new List<(string FileName, bool IsValid)>();

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document inside a using block to ensure proper disposal
            using (Document doc = new Document(pdfPath))
            {
                string logPath = Path.Combine(
                    logFolder,
                    Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

                // Validate against PDF/A‑1B (any PdfFormat can be used as needed)
                bool valid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                results.Add((Path.GetFileName(pdfPath), valid));
            }
        }

        Console.WriteLine("Compliance Dashboard");
        Console.WriteLine(new string('-', 22));
        foreach (var r in results)
        {
            int percent = r.IsValid ? 100 : 0;
            Console.WriteLine($"{r.FileName}: {percent}%");
        }

        if (results.Count > 0)
        {
            double overall = results.Average(r => r.IsValid ? 100.0 : 0.0);
            Console.WriteLine($"Overall compliance: {overall:F1}%");
        }
        else
        {
            Console.WriteLine("No PDF files found in the specified folder.");
        }
    }
}
