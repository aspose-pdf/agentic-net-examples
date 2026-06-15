using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists before attempting to read metadata
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo provides access to custom metadata via GetMetaInfo
        // It implements IDisposable, so wrap it in a using block for deterministic cleanup
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the value of the custom property "ReviewedBy"
            string reviewedBy = pdfInfo.GetMetaInfo("ReviewedBy");

            // Log the result; indicate if the property is missing
            if (string.IsNullOrEmpty(reviewedBy))
            {
                Console.WriteLine("ReviewedBy metadata not found.");
            }
            else
            {
                Console.WriteLine($"ReviewedBy: {reviewedBy}");
            }
        }
    }
}