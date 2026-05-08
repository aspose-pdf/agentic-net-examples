using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class PdfValidationDashboard
{
    static void Main()
    {
        // Input folder containing PDFs to validate
        const string inputFolder = "PdfFiles";
        // Folder where XML validation logs will be stored
        const string logFolder = "ValidationLogs";

        // Ensure both folders exist (creates them if they are missing)
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(logFolder);

        // Collect results: file name -> compliance percentage
        var results = new List<(string FileName, double Compliance)>();

        // Get all PDF files in the input folder (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Load the PDF document inside a using block (ensures disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Prepare the log file path (XML format)
                string logPath = Path.Combine(
                    logFolder,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_log.xml");

                // Validate the document; the method writes an XML log
                // Use a valid PdfFormat enum value (e.g., PDF/UA 1.0)
                bool isValid = doc.Validate(logPath, PdfFormat.PDF_UA_1);

                double compliance;

                if (isValid)
                {
                    // Validation succeeded – assume full compliance
                    compliance = 100.0;
                }
                else
                {
                    // Validation failed – attempt to compute a simple compliance metric
                    // by counting the number of <Error> elements in the XML log.
                    // If the log cannot be read, fall back to 0% compliance.
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(logPath);
                        // Count total error nodes (the exact node name may vary;
                        // adjust as needed for real log structure)
                        int errorCount = xmlDoc.GetElementsByTagName("Error").Count;
                        // For demonstration, treat any error as 0% compliance.
                        // More sophisticated calculations can be added here.
                        compliance = errorCount == 0 ? 100.0 : 0.0;
                    }
                    catch
                    {
                        compliance = 0.0;
                    }
                }

                results.Add((Path.GetFileName(pdfPath), compliance));
            }
        }

        // Generate a simple console dashboard
        Console.WriteLine("PDF Validation Dashboard");
        Console.WriteLine(new string('=', 30));

        foreach (var result in results)
        {
            Console.WriteLine($"{result.FileName}: {result.Compliance:F1}% compliant");
        }

        // Overall average compliance
        double average = results.Any() ? results.Average(r => r.Compliance) : 0.0;
        Console.WriteLine(new string('-', 30));
        Console.WriteLine($"Overall compliance: {average:F1}%");
    }
}
