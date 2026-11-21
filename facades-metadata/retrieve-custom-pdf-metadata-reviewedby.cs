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

        // PdfFileInfo provides access to document metadata without loading the full PDF.
        PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

        // Retrieve the custom metadata entry "ReviewedBy".
        string reviewedBy = pdfInfo.GetMetaInfo("ReviewedBy");

        // Log the value for audit purposes.
        Console.WriteLine($"ReviewedBy: {reviewedBy ?? "(not set)"}");
    }
}