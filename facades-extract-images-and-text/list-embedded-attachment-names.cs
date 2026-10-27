using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // List the names of all embedded attachments (no file saving)
            if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                {
                    Console.WriteLine($"Attachment: {fileSpec.Name}");
                }
            }
            else
            {
                Console.WriteLine("No embedded attachments found.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
