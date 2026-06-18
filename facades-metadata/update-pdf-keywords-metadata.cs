using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string newKeywords   = "Aspose, PDF, Metadata";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF as a stream
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        {
            // Initialize PdfFileInfo with the PDF stream
            using (PdfFileInfo info = new PdfFileInfo(pdfStream))
            {
                // Update the Keywords metadata
                info.Keywords = newKeywords;

                // Save the PDF with the updated metadata to a new file
                info.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"Keywords updated and saved to '{outputPdfPath}'.");
    }
}