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

        // PdfFileInfo gives access to document metadata without loading the full PDF.
        string creator;
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            creator = info.Creator; // Retrieve the Creator metadata
        }

        Console.WriteLine($"Creator: {creator}");
    }
}