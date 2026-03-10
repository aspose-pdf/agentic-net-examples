using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the input SVG and the intermediate PDF
        const string svgPath = "input.svg";
        const string pdfPath = "intermediate.pdf";

        if (!File.Exists(svgPath))
        {
            Console.Error.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert the SVG document to PDF (required because PdfFileInfo works on PDF)
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(svgPath, new SvgLoadOptions()))
        {
            // Save the converted PDF to a temporary file
            pdfDoc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Retrieve PDF metadata using the PdfFileInfo facade
        // -----------------------------------------------------------------
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            Console.WriteLine("PDF Metadata extracted via Aspose.Pdf.Facades:");
            Console.WriteLine($"Title          : {pdfInfo.Title}");
            Console.WriteLine($"Author         : {pdfInfo.Author}");
            Console.WriteLine($"Subject        : {pdfInfo.Subject}");
            Console.WriteLine($"Keywords       : {pdfInfo.Keywords}");
            Console.WriteLine($"Creator        : {pdfInfo.Creator}");
            Console.WriteLine($"Producer       : {pdfInfo.Producer}");
            Console.WriteLine($"CreationDate   : {pdfInfo.CreationDate}");
            Console.WriteLine($"ModDate        : {pdfInfo.ModDate}");
            Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Is Encrypted   : {pdfInfo.IsEncrypted}");
            Console.WriteLine($"PDF Version    : {pdfInfo.GetPdfVersion()}");
        }

        // Optional cleanup of the temporary PDF file
        try
        {
            if (File.Exists(pdfPath))
                File.Delete(pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary PDF: {ex.Message}");
        }
    }
}