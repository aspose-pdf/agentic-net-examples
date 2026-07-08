using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream memoryStream = new MemoryStream(pdfBytes))
        {
            // Initialize the PdfFileInfo facade with the stream
            using (PdfFileInfo pdfInfo = new PdfFileInfo(memoryStream))
            {
                // Modify metadata properties
                pdfInfo.Title = "Updated Document Title";
                pdfInfo.Author = "Jane Smith";
                pdfInfo.Subject = "Demonstration of PdfFileInfo";
                pdfInfo.Keywords = "Aspose.Pdf, Metadata, Example";

                // Save the updated PDF to a new file
                bool saved = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(saved ? $"PDF saved to '{outputPath}'." : "Failed to save updated PDF.");
            }
        }
    }
}