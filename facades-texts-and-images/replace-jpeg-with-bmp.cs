using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF containing the JPEG image
        const string outputPdfPath = "output.pdf";     // PDF after replacement
        const string bmpImagePath  = "highres.bmp";    // path to the high‑resolution BMP file

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bmpImagePath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the document to PdfContentEditor (facade) also within a using block
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(pdfDoc);

                // Replace the first image (index = 1) on page 1 with the BMP file.
                // PdfContentEditor.ReplaceImage expects a file path, so we provide the BMP path directly.
                editor.ReplaceImage(pageNumber: 1, index: 1, imageFile: bmpImagePath);
            }

            // Save the modified document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}