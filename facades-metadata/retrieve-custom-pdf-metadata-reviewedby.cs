using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
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
            Console.WriteLine($"ReviewedBy: {(string.IsNullOrEmpty(reviewedBy) ? "(not set)" : reviewedBy)}");
        }
    }
}