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

        // Initialize PdfFileInfo facade for the PDF file
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve the custom metadata value for "ProjectCode"
            string projectCode = info.GetMetaInfo("ProjectCode");

            // Display the retrieved value (empty string if not present)
            Console.WriteLine($"ProjectCode: {projectCode}");
        }
    }
}