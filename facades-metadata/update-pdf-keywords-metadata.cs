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

        // Open the PDF file as a stream
        using (FileStream pdfStream = File.OpenRead(inputPath))
        {
            // Initialize PdfFileInfo with the PDF stream
            PdfFileInfo fileInfo = new PdfFileInfo(pdfStream);

            // Update the Keywords metadata
            fileInfo.Keywords = "Updated, Keywords, Example";

            // Save the PDF with the updated metadata to a new file
            fileInfo.SaveNewInfo(outputPath);

            // Release resources held by the facade
            fileInfo.Close();
        }

        Console.WriteLine($"Keywords updated and saved to '{outputPath}'.");
    }
}