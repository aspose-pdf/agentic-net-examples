using System;
using System.IO;
using System.Text;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input directory containing PDF files to validate
        const string inputDirectory = "pdfs";

        // Directory where individual XML validation logs will be stored
        const string logDirectory = "validation_logs";

        // Path for the summary CSV file
        const string summaryCsvPath = "validation_summary.csv";

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to process.");
            return;
        }

        // Ensure the log directory exists
        Directory.CreateDirectory(logDirectory);

        // Prepare CSV header
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,IsValid,LogPath");

        // Process each PDF file in the input directory
        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfFilePath);
            string logFilePath = Path.Combine(
                logDirectory,
                Path.GetFileNameWithoutExtension(pdfFilePath) + ".xml");

            bool isValid = false;

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFilePath))
                {
                    // Validate the document against PDF/A-1B compliance.
                    // The method returns true if the document complies, false otherwise.
                    // Validation details are written to the specified XML log file.
                    isValid = doc.Validate(logFilePath, PdfFormat.PDF_A_1B);
                }
            }
            catch (Exception ex)
            {
                // If loading or validation throws, treat the document as invalid
                // and write a simple error entry to the XML log.
                isValid = false;
                string errorXml = $"<validation><error>{System.Security.SecurityElement.Escape(ex.Message)}</error></validation>";
                File.WriteAllText(logFilePath, errorXml);
            }

            // Escape commas in CSV fields if necessary
            string escapedFileName = fileName.Contains(",") ? $"\"{fileName}\"" : fileName;
            string escapedLogPath = logFilePath.Contains(",") ? $"\"{logFilePath}\"" : logFilePath;

            // Append the result line to the CSV content
            csvBuilder.AppendLine($"{escapedFileName},{isValid},{escapedLogPath}");
        }

        // Write the accumulated CSV data to the summary file
        File.WriteAllText(summaryCsvPath, csvBuilder.ToString());

        Console.WriteLine($"Validation completed. Summary CSV saved to '{summaryCsvPath}'.");
    }
}
