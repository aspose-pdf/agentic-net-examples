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

        // Initialize the PdfFileInfo facade for the PDF document
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the custom metadata value for "ProjectCode"
            string projectCode = pdfInfo.GetMetaInfo("ProjectCode");

            // Display the result
            if (string.IsNullOrEmpty(projectCode))
                Console.WriteLine("ProjectCode metadata not found.");
            else
                Console.WriteLine($"ProjectCode: {projectCode}");
        }
    }
}