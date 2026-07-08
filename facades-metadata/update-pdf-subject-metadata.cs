using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newSubject = "Report for Q4 Financial Analysis";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF metadata using PdfFileInfo, modify the Subject, and save the updated file.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Set the Subject property to reflect the document's purpose.
            pdfInfo.Subject = newSubject;

            // Save the updated metadata into a new PDF file.
            // SaveNewInfo writes only the changed info without re‑creating the whole document.
            bool success = pdfInfo.SaveNewInfo(outputPdf);
            Console.WriteLine(success
                ? $"Subject updated and saved to '{outputPdf}'."
                : $"Failed to save updated PDF to '{outputPdf}'.");
        }
    }
}