using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF file as a read‑only stream
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        {
            // Initialize PdfFileInfo with the PDF stream
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfStream))
            {
                // Update the Keywords metadata
                pdfInfo.Keywords = "Updated, Keywords, Example";

                // Save the PDF with the new metadata to a new file
                pdfInfo.SaveNewInfo(outputPdfPath);
            }
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdfPath}'.");
    }
}