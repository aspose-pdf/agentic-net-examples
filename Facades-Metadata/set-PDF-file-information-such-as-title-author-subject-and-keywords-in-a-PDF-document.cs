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

        // Load the PDF file using the Facades PdfFileInfo class
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set the desired document information
            pdfInfo.Title = "Sample Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demo Subject";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}