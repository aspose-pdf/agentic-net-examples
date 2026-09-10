using System;
using System.IO;
using Aspose.Pdf;

class PdfBatchValidator
{
    static void Main(string[] args)
    {
        // Input directory containing PDFs (use first argument or default)
        string inputDirectory = args.Length > 0 ? args[0] : "PdfFiles";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Directory to store individual XML validation logs
        string logDirectory = Path.Combine(inputDirectory, "ValidationLogs");
        Directory.CreateDirectory(logDirectory);

        // Path for the summary CSV file
        string csvPath = Path.Combine(inputDirectory, "validation_summary.csv");

        // Write CSV header
        using (StreamWriter csvWriter = new StreamWriter(csvPath))
        {
            csvWriter.WriteLine("FileName,IsValid,LogFile");

            // Process each PDF file in the directory
            foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfPath);
                string logFile = Path.Combine(logDirectory,
                    Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

                bool isValid = false;

                // Load the PDF and perform validation
                using (Document doc = new Document(pdfPath))
                {
                    // Validate against PDF/A-1B format and write XML log
                    isValid = doc.Validate(logFile, PdfFormat.PDF_A_1B);
                }

                // Record result in CSV
                csvWriter.WriteLine($"{fileName},{isValid},{logFile}");
                Console.WriteLine($"Validated: {fileName} – Valid={isValid}");
            }
        }

        Console.WriteLine($"Validation summary written to: {csvPath}");
    }
}