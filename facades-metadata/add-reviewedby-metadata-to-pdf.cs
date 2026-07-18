using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string reviewer  = "John Doe";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF for metadata manipulation using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Set a custom metadata field named "ReviewedBy"
            pdfInfo.SetMetaInfo("ReviewedBy", reviewer);

            // Persist the changes to a new file
            bool success = pdfInfo.SaveNewInfo(outputPdf);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }
        }

        Console.WriteLine($"Custom metadata 'ReviewedBy' added and saved to '{outputPdf}'.");
    }
}