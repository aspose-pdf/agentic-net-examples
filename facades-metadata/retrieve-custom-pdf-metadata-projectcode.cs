using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists before proceeding.
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfFileInfo facade with the PDF file.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the custom metadata value for "ProjectCode".
            string projectCode = pdfInfo.GetMetaInfo("ProjectCode");

            // Display the retrieved value (empty string if the key does not exist).
            Console.WriteLine($"ProjectCode: {projectCode}");
        }
    }
}