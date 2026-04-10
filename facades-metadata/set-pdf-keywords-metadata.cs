using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_keywords.pdf";
        const string keywords  = "Aspose.Pdf, Metadata, Keywords";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, set the Keywords metadata, and save the updated file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            pdfInfo.Keywords = keywords;                     // set Keywords property
            bool saved = pdfInfo.SaveNewInfo(outputPdf);     // save updated PDF
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }
        }

        // Verify that the Keywords were written correctly
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPdf))
        {
            string savedKeywords = verifyInfo.Keywords;
            Console.WriteLine($"Keywords after save: \"{savedKeywords}\"");
            Console.WriteLine(savedKeywords == keywords
                ? "Verification succeeded."
                : "Verification failed: Keywords do not match.");
        }
    }
}