using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source TeX file and the target PDF file
        const string texFilePath = "input.tex";
        const string pdfOutputPath = "output.pdf";

        // Verify that the TeX source exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX file not found: {texFilePath}");
            return;
        }

        // Load the TeX file and convert it to a PDF Document
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document pdfDocument = new Document(texFilePath, texLoadOptions))
        {
            // Create a PdfFileInfo facade bound to the PDF document
            PdfFileInfo fileInfo = new PdfFileInfo(pdfDocument);

            // Set the desired PDF metadata
            fileInfo.Title    = "My TeX Document Title";
            fileInfo.Author   = "Jane Smith";
            fileInfo.Subject  = "Demonstration of PDF metadata via Aspose.Pdf.Facades";
            fileInfo.Keywords = "Aspose.Pdf, TeX, metadata, PDF";

            // Save the PDF with the updated information
            fileInfo.SaveNewInfo(pdfOutputPath);
        }

        Console.WriteLine($"PDF created with metadata: {pdfOutputPath}");
    }
}