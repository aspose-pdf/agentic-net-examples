using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newKeywords = "Confidential; Sample";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize PdfFileInfo with the stream containing PDF data
            PdfFileInfo fileInfo = new PdfFileInfo(pdfStream);

            // Update the Keywords metadata field
            fileInfo.Keywords = newKeywords;

            // Save the PDF with the updated metadata to a new file
            fileInfo.Save(outputPath);
        }

        Console.WriteLine($"Keywords updated and saved to '{outputPath}'.");
    }
}