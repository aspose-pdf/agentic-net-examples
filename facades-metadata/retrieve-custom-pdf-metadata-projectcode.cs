using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfFileInfo facade for the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the custom metadata value for "ProjectCode"
            string projectCode = pdfInfo.GetMetaInfo("ProjectCode");

            // Display the retrieved value (empty string if the property does not exist)
            Console.WriteLine($"ProjectCode: {projectCode}");
        }
    }
}