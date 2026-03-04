using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the input TeX file and the output PDF file
        const string texPath   = "input.tex";
        const string pdfPath   = "output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }

        // Load the TeX file and convert it to a PDF document using TeXLoadOptions
        using (Document pdfDocument = new Document(texPath, new TeXLoadOptions()))
        {
            // Create a PdfFileInfo facade bound to the generated PDF document
            using (PdfFileInfo fileInfo = new PdfFileInfo(pdfDocument))
            {
                // Modify desired PDF document properties
                fileInfo.Title  = "Modified PDF Title";
                fileInfo.Author = "John Doe";
                fileInfo.Subject = "Generated from TeX";
                fileInfo.Keywords = "Aspose.Pdf, TeX, PDF";

                // Save the updated PDF document with the new metadata
                fileInfo.SaveNewInfo(pdfPath);
            }
        }

        Console.WriteLine($"PDF created and properties updated: {pdfPath}");
    }
}