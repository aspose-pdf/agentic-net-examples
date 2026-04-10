using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Open a read‑only stream for the source PDF
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        {
            // Initialize PdfFileInfo with the PDF stream
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfStream))
            {
                // Update the Keywords metadata
                pdfInfo.Keywords = "Updated Keywords";

                // Save the PDF with the modified metadata to a new file
                pdfInfo.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with updated keywords to '{outputPdfPath}'.");
    }
}