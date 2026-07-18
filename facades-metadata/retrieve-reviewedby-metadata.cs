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

        // PdfFileInfo is a facade for accessing PDF file information, including custom metadata
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Retrieve the value of the custom metadata property "ReviewedBy"
            string reviewedBy = fileInfo.GetMetaInfo("ReviewedBy");

            // Log the retrieved value; an empty string indicates the property does not exist
            if (string.IsNullOrEmpty(reviewedBy))
                Console.WriteLine("ReviewedBy metadata not found.");
            else
                Console.WriteLine($"ReviewedBy: {reviewedBy}");
        }
    }
}