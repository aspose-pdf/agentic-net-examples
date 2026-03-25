using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDF files
        const string inputDir = "pdfs";
        // Directory where XML validation logs will be stored
        const string logDir = "validation-logs";
        // Path for the summary CSV file
        const string csvPath = "validation_summary.csv";

        // Ensure the log directory exists
        Directory.CreateDirectory(logDir);

        // Verify the input directory exists before proceeding
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory '{inputDir}' not found. No PDFs to validate.");
            return;
        }

        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,IsValid,LogFile");

        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string logPath = Path.Combine(logDir, Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

            bool isValid = false;
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // Validate against PDF/A‑1B (example); the method returns the validation result
                    isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                }
            }
            catch (Exception ex)
            {
                // If validation throws, treat as invalid and write the exception to the log file
                isValid = false;
                File.WriteAllText(logPath, $"<Error>{ex.Message}</Error>");
            }

            csvBuilder.AppendLine($"{fileName},{isValid},{logPath}");
        }

        File.WriteAllText(csvPath, csvBuilder.ToString());
        Console.WriteLine($"Validation summary saved to '{csvPath}'.");
    }
}
