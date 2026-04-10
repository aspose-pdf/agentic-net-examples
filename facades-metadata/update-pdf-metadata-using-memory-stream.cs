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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream memory = new MemoryStream(pdfBytes))
        {
            // Initialize the PdfFileInfo facade with the stream
            PdfFileInfo fileInfo = new PdfFileInfo(memory);

            // Modify metadata properties
            fileInfo.Title    = "Updated Title";
            fileInfo.Author   = "John Doe";
            fileInfo.Subject  = "Sample Subject";
            fileInfo.Keywords = "Aspose, PDF, Metadata";

            // Save the updated PDF to a new file
            bool saved = fileInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved ? $"Metadata updated and saved to '{outputPath}'." : "Failed to save updated PDF.");

            // Release resources held by the facade
            fileInfo.Close();
        }
    }
}