using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to validate
        const string inputFolder = "pdfs";
        // Folder where XML validation logs will be written
        const string logFolder = "validation-logs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(logFolder);

        var results = new List<(string FileName, bool IsValid)>();

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string logPath = Path.Combine(logFolder, Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Validate against PDF/A‑1B; the method writes an XML log file.
                    bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                    results.Add((fileName, isValid));
                    Console.WriteLine($"{fileName}: {(isValid ? "Compliant" : "Non‑compliant")} (log: {logPath})");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
                results.Add((fileName, false));
            }
        }

        int total = results.Count;
        int compliant = results.Count(r => r.IsValid);
        double compliancePct = total > 0 ? (double)compliant / total * 100 : 0;

        Console.WriteLine();
        Console.WriteLine($"Processed {total} file(s).");
        Console.WriteLine($"Compliance: {compliant}/{total} ({compliancePct:F2}%)");
    }
}