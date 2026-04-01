using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputDirectory = "pdfs";
        string summaryFile = "summary.txt";

        // Ensure the input directory exists
        Directory.CreateDirectory(inputDirectory);

        using (StreamWriter writer = new StreamWriter(summaryFile, false))
        {
            writer.WriteLine("FileName,Encrypted,Algorithm,Privileges");

            string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
            foreach (string pdfPath in pdfFiles)
            {
                string fileName = Path.GetFileName(pdfPath);
                bool isEncrypted = false;
                string algorithm = "None";
                string privileges = "None";

                try
                {
                    // Attempt to open the PDF without a password
                    using (Document doc = new Document(pdfPath))
                    {
                        // If no exception, the file is not encrypted
                        isEncrypted = false;
                        algorithm = "None";
                        privileges = "None";
                    }
                }
                catch (InvalidPasswordException)
                {
                    // The PDF is encrypted; use PdfFileSecurity to bind it
                    isEncrypted = true;
                    PdfFileSecurity security = new PdfFileSecurity();
                    security.BindPdf(pdfPath);
                    // Aspose.Pdf does not expose algorithm/privilege getters directly.
                    // Placeholders are used; they can be replaced with proper API calls if available.
                    algorithm = "Unknown";
                    privileges = "Unknown";
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
                    continue;
                }

                writer.WriteLine($"{fileName},{isEncrypted},{algorithm},{privileges}");
                Console.WriteLine($"{fileName}: Encrypted={isEncrypted}");
            }
        }

        Console.WriteLine($"Summary written to {summaryFile}");
    }
}