using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Output CSV file name (must be a simple filename as per style rules)
        const string outputCsv = "encryption_summary.csv";

        // Prepare a StringBuilder for CSV content
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,IsEncrypted,Algorithm,Privileges");

        // Get all PDF files in the current directory (non‑recursive for simplicity)
        string[] pdfFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            bool isEncrypted = false;
            string algorithm = "None";
            string privileges = "None";

            // Try to open the PDF normally. If it throws, the file is encrypted.
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    // If we reach here, the document opened without a password – not encrypted.
                    isEncrypted = false;
                    algorithm = "None";
                    privileges = "None";
                }
            }
            catch (PdfException)
            {
                // The document is encrypted. Detailed information (algorithm, privileges) is not exposed
                // directly via the core API, so we record placeholders.
                isEncrypted = true;
                algorithm = "Unknown";
                privileges = "Unknown";
            }
            catch (Exception ex)
            {
                // Any other exception – treat as error and continue.
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
                continue;
            }

            // Append the information to the CSV.
            csvBuilder.AppendLine($"{fileName},{isEncrypted},{algorithm},{privileges}");
        }

        // Write the CSV file.
        try
        {
            File.WriteAllText(outputCsv, csvBuilder.ToString());
            Console.WriteLine($"Encryption summary written to '{outputCsv}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write summary file: {ex.Message}");
        }
    }
}