using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newSubject = "Report on Quarterly Sales";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF metadata using PdfFileInfo, update the Subject, and save the changes
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Set the Subject property to reflect the document's purpose
            pdfInfo.Subject = newSubject;

            // Save the updated metadata into a new PDF file
            bool success = pdfInfo.SaveNewInfo(outputPdf);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save updated PDF metadata.");
                return;
            }
        }

        Console.WriteLine($"Subject updated and saved to '{outputPdf}'.");
    }
}