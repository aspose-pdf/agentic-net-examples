using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the input TeX file and the output PDF file
        const string texPath = "input.tex";
        const string outputPdf = "output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"Error: TeX file not found at '{texPath}'.");
            return;
        }

        // Convert the TeX document to a PDF using the core Document API with TeXLoadOptions
        Document pdfDocument;
        try
        {
            pdfDocument = new Document(texPath, new TeXLoadOptions());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading TeX file: {ex.Message}");
            return;
        }

        // Use the PdfFileInfo facade to set PDF metadata (title, author, subject, keywords)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfDocument))
        {
            pdfInfo.Title = "Sample PDF Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of setting metadata via Aspose.Pdf.Facades";
            pdfInfo.Keywords = "Aspose.Pdf, TeX, metadata, PDF";

            // Save the PDF with the updated metadata
            pdfInfo.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully created with metadata at '{outputPdf}'.");
    }
}