using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, SaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Save the document as PDF (same format). No SaveOptions needed for PDF output.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF successfully copied to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}