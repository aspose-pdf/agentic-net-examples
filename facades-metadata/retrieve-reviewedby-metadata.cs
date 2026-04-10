using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfFileInfo facade for the PDF document
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Retrieve the custom metadata property "ReviewedBy"
            string reviewedBy = pdfInfo.GetMetaInfo("ReviewedBy");

            // Log the value (empty string if the property does not exist)
            Console.WriteLine($"ReviewedBy: {reviewedBy}");
        }
    }
}