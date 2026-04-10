using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newTitle  = "Updated PDF Title";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileInfo facade with the source PDF
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Set the Title metadata
            pdfInfo.Title = newTitle;

            // Save the updated information to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPdf);
            Console.WriteLine(saved
                ? $"Title updated and saved to '{outputPdf}'."
                : $"Failed to save updated PDF to '{outputPdf}'.");
        }
    }
}