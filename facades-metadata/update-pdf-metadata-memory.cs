using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Create a Document from the memory stream
            using (Document doc = new Document(inputStream))
            {
                // Initialize PdfFileInfo with the loaded document
                using (PdfFileInfo info = new PdfFileInfo(doc))
                {
                    // Modify metadata properties
                    info.Title = "Updated Title";
                    info.Author = "John Doe";
                    info.Subject = "Demo of metadata update";
                    info.Keywords = "Aspose, PDF, metadata";

                    // Save the updated PDF to a new file
                    bool success = info.SaveNewInfo(outputPath);
                    Console.WriteLine(success ? $"Metadata updated and saved to {outputPath}" : "Failed to save updated PDF");
                }
            }
        }
    }
}