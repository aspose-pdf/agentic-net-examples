using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceImageExample
{
    static void Main()
    {
        // Paths for the source PDF, the high‑resolution BMP stream source, and the output PDF.
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string bmpFilePath   = "high_res_image.bmp"; // BMP file on disk (could be a stream source).

        // Verify that the input files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bmpFilePath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpFilePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the PdfContentEditor facade and bind it to the loaded document.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfDoc);

            // Replace the first image (index = 1) on the first page (pageNumber = 1)
            // with the higher‑resolution BMP image. ReplaceImage expects a file path.
            editor.ReplaceImage(pageNumber: 1, index: 1, imageFile: bmpFilePath);

            // Save the modified PDF to the output path.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}