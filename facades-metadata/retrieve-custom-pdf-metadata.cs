using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides access to document metadata without loading the full PDF
        PdfFileInfo info = new PdfFileInfo(pdfPath);

        // Retrieve the custom metadata entry named "ProjectCode"
        string projectCode = info.GetMetaInfo("ProjectCode");

        // Display the retrieved value (may be null or empty if not set)
        Console.WriteLine($"ProjectCode: {projectCode ?? "(not set)"}");
    }
}