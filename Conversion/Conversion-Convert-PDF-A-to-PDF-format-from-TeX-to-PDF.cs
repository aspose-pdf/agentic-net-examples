using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source TeX file, intermediate PDF/A file, and final PDF file
        const string texFilePath = "input.tex";
        const string intermediatePdfPath = "intermediate.pdf";
        const string finalPdfPath = "output.pdf";

        // Ensure the source TeX file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX source not found: {texFilePath}");
            return;
        }

        // Load the TeX document using TeXLoadOptions and save it as PDF (initially may be PDF/A)
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document texDoc = new Document(texFilePath, texLoadOptions))
        {
            texDoc.Save(intermediatePdfPath); // Saves as PDF regardless of extension
        }

        // Load the generated PDF (which could be PDF/A) and remove PDF/A compliance
        using (Document pdfDoc = new Document(intermediatePdfPath))
        {
            // Remove PDF/A compliance if present
            pdfDoc.RemovePdfaCompliance();

            // Save as a regular PDF file
            pdfDoc.Save(finalPdfPath);
        }

        Console.WriteLine($"Conversion completed. Output PDF: {finalPdfPath}");
    }
}