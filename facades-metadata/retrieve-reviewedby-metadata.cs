using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the PdfFileInfo facade for the specified PDF
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the custom metadata value named "ReviewedBy"
            string reviewedBy = pdfInfo.GetMetaInfo("ReviewedBy");

            // Log the retrieved value; if the property is missing, an empty string is returned
            Console.WriteLine($"ReviewedBy: {(string.IsNullOrEmpty(reviewedBy) ? "(not set)" : reviewedBy)}");
        }
    }
}