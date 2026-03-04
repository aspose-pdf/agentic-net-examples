using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the XSL‑FO source and the temporary PDF output
        const string xslFoPath   = @"C:\Data\sample.xslfo";
        const string tempPdfPath = @"C:\Data\temp_output.pdf";

        // Verify source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert XSL‑FO to PDF using XslFoLoadOptions
        // -----------------------------------------------------------------
        XslFoLoadOptions loadOptions = new XslFoLoadOptions(); // no external XSL required
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Save the intermediate PDF (required for PdfFileInfo which works on a file/stream)
            pdfDoc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // 2. Read PDF document properties via PdfFileInfo facade
        // -----------------------------------------------------------------
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(tempPdfPath); // Load the PDF created from XSL‑FO

        // Access standard metadata properties
        Console.WriteLine($"Title          : {pdfInfo.Title}");
        Console.WriteLine($"Author         : {pdfInfo.Author}");
        Console.WriteLine($"Subject        : {pdfInfo.Subject}");
        Console.WriteLine($"Keywords       : {pdfInfo.Keywords}");
        Console.WriteLine($"Creator        : {pdfInfo.Creator}");
        Console.WriteLine($"Producer       : {pdfInfo.Producer}");
        Console.WriteLine($"Creation Date  : {pdfInfo.CreationDate}");
        Console.WriteLine($"Modification   : {pdfInfo.ModDate}");
        Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
        Console.WriteLine($"PDF Version    : {pdfInfo.GetPdfVersion()}");
        Console.WriteLine($"Is Encrypted   : {pdfInfo.IsEncrypted}");
        Console.WriteLine($"Is PDF File    : {pdfInfo.IsPdfFile}");

        // Clean up the temporary PDF file
        try
        {
            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary file: {ex.Message}");
        }
    }
}