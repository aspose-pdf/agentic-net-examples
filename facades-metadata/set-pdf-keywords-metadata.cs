using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_keywords.pdf";
        const string keywords  = "Aspose, PDF, Metadata";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, set the Keywords metadata, and save to a new file
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            // Set the Keywords property
            info.Keywords = keywords;

            // Save the updated PDF; SaveNewInfo returns true on success
            bool saved = info.SaveNewInfo(outputPdf);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }
        }

        // Verify that the Keywords were written correctly
        using (PdfFileInfo verifyInfo = new PdfFileInfo(outputPdf))
        {
            Console.WriteLine($"Keywords after save: {verifyInfo.Keywords}");
        }
    }
}