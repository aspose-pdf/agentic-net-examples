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

        // Load the PDF into a memory stream
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Bind the stream to PdfFileInfo for metadata manipulation
            PdfFileInfo pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputStream);

            // Modify metadata fields
            pdfInfo.Title = "Updated Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Save the updated PDF to a new file
            pdfInfo.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}