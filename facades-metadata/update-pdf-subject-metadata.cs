using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // PDF with updated Subject
        const string newSubject = "Report for Q3 2026";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize PdfFileInfo facade with the source file
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            // Update the Subject metadata
            info.Subject = newSubject;

            // Save the updated information into a new PDF file
            bool success = info.SaveNewInfo(outputPdf);
            Console.WriteLine(success
                ? $"Subject updated and saved to '{outputPdf}'."
                : $"Failed to save updated PDF to '{outputPdf}'.");
        }
    }
}