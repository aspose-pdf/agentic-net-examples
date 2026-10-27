using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class PdfBatchValidator
{
    static void Main(string[] args)
    {
        // Input directory containing PDFs; use first argument or current directory if none provided
        string inputDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Prepare summary CSV file
        string summaryCsvPath = Path.Combine(inputDirectory, "validation_summary.csv");
        using (StreamWriter csvWriter = new StreamWriter(summaryCsvPath, false))
        {
            // CSV header
            csvWriter.WriteLine("FileName,IsValid,LogPath");

            // Process each PDF file in the directory
            foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
            {
                bool isValid = false;
                string logPath = Path.Combine(
                    inputDirectory,
                    Path.GetFileNameWithoutExtension(pdfPath) + ".xml");

                // Attempt to load the PDF; successful load indicates basic structural validity
                try
                {
                    using (Document doc = new Document(pdfPath))
                    {
                        // If we reach this point, the PDF was opened without exception
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {
                    // Loading failed – treat as invalid and capture the exception message
                    isValid = false;
                    // Include exception details in the XML log
                    XDocument errorLog = new XDocument(
                        new XElement("Validation",
                            new XElement("File", Path.GetFileName(pdfPath)),
                            new XElement("Status", "Invalid"),
                            new XElement("Error", ex.Message)));
                    errorLog.Save(logPath);
                    csvWriter.WriteLine($"{Path.GetFileName(pdfPath)},{isValid},{logPath}");
                    continue; // Move to next file
                }

                // Create a simple XML log indicating validation result
                XDocument xmlLog = new XDocument(
                    new XElement("Validation",
                        new XElement("File", Path.GetFileName(pdfPath)),
                        new XElement("Status", isValid ? "Valid" : "Invalid")));
                xmlLog.Save(logPath);

                // Write entry to the summary CSV
                csvWriter.WriteLine($"{Path.GetFileName(pdfPath)},{isValid},{logPath}");
            }
        }

        Console.WriteLine($"Validation complete. Summary CSV saved to: {summaryCsvPath}");
    }
}