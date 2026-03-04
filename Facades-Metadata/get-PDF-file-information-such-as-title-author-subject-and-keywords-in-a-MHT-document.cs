using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source MHT file
        const string mhtPath = "input.mht";

        // Temporary PDF file to hold the converted document
        const string tempPdfPath = "temp.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Convert MHT to PDF using Document with MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(mhtPath, loadOptions))
        {
            // Save the converted PDF to a temporary file
            pdfDoc.Save(tempPdfPath);
        }

        // Use PdfFileInfo facade to read PDF metadata
        PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath);

        Console.WriteLine($"Title   : {pdfInfo.Title}");
        Console.WriteLine($"Author  : {pdfInfo.Author}");
        Console.WriteLine($"Subject : {pdfInfo.Subject}");
        Console.WriteLine($"Keywords: {pdfInfo.Keywords}");

        // Clean up the temporary PDF file
        if (File.Exists(tempPdfPath))
        {
            File.Delete(tempPdfPath);
        }
    }
}