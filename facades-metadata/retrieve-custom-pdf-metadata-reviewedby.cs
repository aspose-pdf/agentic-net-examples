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
        PdfFileInfo info = new PdfFileInfo(pdfPath);

        // Retrieve the custom metadata entry named "ReviewedBy".
        string reviewedBy = info.GetMetaInfo("ReviewedBy");

        // Log the result for audit purposes.
        if (!string.IsNullOrEmpty(reviewedBy))
        {
            Console.WriteLine($"ReviewedBy: {reviewedBy}");
        }
        else
        {
            Console.WriteLine("ReviewedBy metadata not found.");
        }
    }
}