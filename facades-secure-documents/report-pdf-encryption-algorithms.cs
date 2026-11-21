using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to inspect
        const string pdfFolder = "pdfs";
        // Path for the generated report
        const string reportFile = "encryption_report.csv";

        if (!Directory.Exists(pdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {pdfFolder}");
            return;
        }

        // Create (or overwrite) the CSV report
        using (StreamWriter writer = new StreamWriter(reportFile, false))
        {
            writer.WriteLine("File,Encrypted,Algorithm");

            // Iterate over all PDF files in the folder
            foreach (string pdfPath in Directory.GetFiles(pdfFolder, "*.pdf"))
            {
                bool isEncrypted = false;
                string algorithm = "N/A"; // Aspose.PDF does not expose the algorithm directly

                // Detect encryption by attempting to load the document.
                // If the PDF is encrypted, Aspose.Pdf throws a PdfException.
                try
                {
                    using (var doc = new Document(pdfPath))
                    {
                        // Document loaded successfully – not encrypted.
                        isEncrypted = false;
                    }
                }
                catch (PdfException)
                {
                    // Loading failed because the PDF is encrypted.
                    isEncrypted = true;
                }
                catch (Exception ex)
                {
                    // Any other exception (e.g., corrupted file) is reported but does not stop processing.
                    Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
                    continue;
                }

                // Write CSV line
                writer.WriteLine($"{Path.GetFileName(pdfPath)},{isEncrypted},{algorithm}");

                // Optional console output for immediate feedback
                Console.WriteLine($"{Path.GetFileName(pdfPath)}: Encrypted={isEncrypted}, Algorithm={algorithm}");
            }
        }

        Console.WriteLine($"Encryption report saved to '{reportFile}'.");
    }
}
