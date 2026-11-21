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

        // PdfFileInfo implements IDisposable, so wrap it in a using block
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Access the Creator metadata field
            string creator = fileInfo.Creator;

            // Variable 'creator' now holds the PDF's creator information
            Console.WriteLine($"Creator: {creator}");
        }
    }
}