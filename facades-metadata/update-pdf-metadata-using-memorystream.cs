using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream memory = new MemoryStream(pdfBytes))
        {
            // Initialize the PdfFileInfo facade with the stream
            using (PdfFileInfo fileInfo = new PdfFileInfo(memory))
            {
                // Modify metadata properties
                fileInfo.Title    = "New Document Title";
                fileInfo.Author   = "John Doe";
                fileInfo.Subject  = "Sample Subject";
                fileInfo.Keywords = "Aspose, PDF, Metadata";
                fileInfo.Creator  = "My Application";

                // Save the updated PDF to a new file
                bool saved = fileInfo.SaveNewInfo(outputPath);
                Console.WriteLine(saved
                    ? $"Metadata updated and saved to '{outputPath}'."
                    : "Failed to save the updated PDF.");
            }
        }
    }
}