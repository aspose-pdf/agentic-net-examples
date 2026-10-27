using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfFileInfo implements IDisposable, so wrap it in a using block.
            using (PdfFileInfo info = new PdfFileInfo(pdfPath))
            {
                // Retrieve the Keywords metadata from the PDF.
                string keywords = info.Keywords ?? string.Empty;

                // Display the value.
                Console.WriteLine($"Keywords: {keywords}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error retrieving keywords: {ex.Message}");
        }
    }
}