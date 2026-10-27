using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to validate
        const string inputDirectory = @"C:\PdfBatch";
        // Directory where XML logs will be stored
        const string logDirectory = @"C:\PdfBatch\Logs";
        // Path of the summary CSV file
        const string summaryCsvPath = @"C:\PdfBatch\validation_summary.csv";

        // Ensure the log directory exists
        Directory.CreateDirectory(logDirectory);

        // Prepare CSV header
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,IsValid,ErrorMessage");

        // Enumerate all PDF files in the input directory (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            bool isValid = true;
            string errorMessage = string.Empty;

            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Attempt basic validation by accessing the document structure.
                    // If the document is corrupted, an exception will be thrown.
                    int pageCount = doc.Pages.Count; // forces parsing of the page tree

                    // Optional: call the built‑in Validate method if available.
                    // Uncomment the following lines if your Aspose.Pdf version supports it.
                    // var validationResult = doc.Validate();
                    // isValid = validationResult.IsValid;
                }
            }
            catch (Exception ex)
            {
                // Any exception means the PDF is not valid
                isValid = false;
                errorMessage = ex.Message;
            }

            // Create an XML log for this file
            XDocument logDoc = new XDocument(
                new XElement("Validation",
                    new XElement("FileName", fileName),
                    new XElement("IsValid", isValid),
                    new XElement("ErrorMessage", errorMessage)
                )
            );

            // Save the XML log (extension .xml) in the log directory
            string logPath = Path.Combine(logDirectory, Path.ChangeExtension(fileName, ".xml"));
            logDoc.Save(logPath);

            // Append result to CSV summary
            // Escape double quotes in the error message
            string escapedError = errorMessage.Replace("\"", "\"\"");
            csvBuilder.AppendLine($"{fileName},{isValid},\"{escapedError}\"");
        }

        // Write the CSV summary file
        File.WriteAllText(summaryCsvPath, csvBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine("Validation complete.");
        Console.WriteLine($"XML logs saved to: {logDirectory}");
        Console.WriteLine($"Summary CSV saved to: {summaryCsvPath}");
    }
}