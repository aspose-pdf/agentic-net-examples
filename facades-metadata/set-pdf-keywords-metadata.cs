using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string keywords = "Aspose, PDF, Metadata";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Set the Keywords metadata and save to a new file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            pdfInfo.Keywords = keywords;
            bool success = pdfInfo.SaveNewInfo(outputPdf);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save the PDF with updated metadata.");
                return;
            }
        }

        // Verify that the Keywords were saved correctly
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPdf))
        {
            string savedKeywords = verifyInfo.Keywords;
            Console.WriteLine($"Keywords after save: '{savedKeywords}'");
        }
    }
}