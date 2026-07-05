using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Ensure the file exists before processing
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo implements IDisposable, so wrap it in a using block
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Retrieve the custom metadata property "ReviewedBy"
            string reviewedBy = pdfInfo.GetMetaInfo("ReviewedBy");

            // Log the result; empty string indicates the property is not present
            if (string.IsNullOrEmpty(reviewedBy))
                Console.WriteLine("Custom metadata 'ReviewedBy' not found.");
            else
                Console.WriteLine($"ReviewedBy: {reviewedBy}");
        }
    }
}