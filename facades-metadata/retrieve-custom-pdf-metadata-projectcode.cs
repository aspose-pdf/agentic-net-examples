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

        // Initialize the PdfFileInfo facade for the specified PDF file
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the custom metadata value associated with the key "ProjectCode"
            string projectCode = fileInfo.GetMetaInfo("ProjectCode");

            // Display the result; an empty string indicates the key was not present
            if (string.IsNullOrEmpty(projectCode))
                Console.WriteLine("ProjectCode metadata not found.");
            else
                Console.WriteLine($"ProjectCode: {projectCode}");
        }
    }
}