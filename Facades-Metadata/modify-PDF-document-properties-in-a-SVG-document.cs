using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string svgPath = "input.svg";
        const string tempPdfPath = "temp.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify SVG input exists
        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // Convert SVG to PDF using Document with SvgLoadOptions
        using (Document pdfDoc = new Document(svgPath, new SvgLoadOptions()))
        {
            pdfDoc.Save(tempPdfPath); // Save intermediate PDF
        }

        // Modify PDF metadata using PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath);
        pdfInfo.Title = "Converted SVG Document";
        pdfInfo.Author = "John Doe";
        pdfInfo.Subject = "SVG to PDF conversion example";
        pdfInfo.Keywords = "SVG, PDF, Aspose.Pdf.Facades";
        pdfInfo.Creator = "Aspose.Pdf.Facades Demo";

        // Save the PDF with updated properties
        pdfInfo.SaveNewInfo(outputPdfPath);

        // Clean up temporary file
        if (File.Exists(tempPdfPath))
        {
            File.Delete(tempPdfPath);
        }

        Console.WriteLine($"PDF with updated properties saved to '{outputPdfPath}'.");
    }
}