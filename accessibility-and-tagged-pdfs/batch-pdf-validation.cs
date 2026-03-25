using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDir = "pdfs";
        string logDir = "validation-logs";
        string csvPath = "validation_summary.csv";

        // Ensure the input directory exists before trying to enumerate files.
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' does not exist. No PDFs to validate.");
            return;
        }

        Directory.CreateDirectory(logDir);

        using (var csvWriter = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            csvWriter.WriteLine("FileName,IsCompliant,LogFile");

            foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfPath);
                string logFile = Path.Combine(logDir, Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

                bool isValid;
                try
                {
                    using (Document doc = new Document(pdfPath))
                    {
                        // Validate against PDF/A‑1B; the method creates an XML log.
                        isValid = doc.Validate(logFile, PdfFormat.PDF_A_1B);
                    }
                }
                catch (Exception ex)
                {
                    // Loading or validation failure – treat as non‑compliant.
                    isValid = false;
                    File.WriteAllText(logFile, $"<error>{ex.Message}</error>");
                }

                csvWriter.WriteLine($"{fileName},{isValid},{logFile}");
            }
        }

        Console.WriteLine($"Validation completed. Summary saved to {csvPath}");
    }
}
