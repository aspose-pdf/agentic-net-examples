using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceImageExample
{
    static void Main()
    {
        // Paths for the source PDF, the high‑resolution BMP source, and the output PDF.
        const string inputPdfPath  = "input.pdf";
        const string bmpSourcePath = "highres.bmp";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bmpSourcePath))
        {
            Console.Error.WriteLine($"BMP source not found: {bmpSourcePath}");
            return;
        }

        // Create a temporary file to hold the BMP data because PdfContentEditor.ReplaceImage
        // expects a file path (it does not accept a Stream directly).
        string tempBmpPath = Path.GetTempFileName();
        try
        {
            // Copy the BMP stream to the temporary file.
            using (FileStream srcStream = File.OpenRead(bmpSourcePath))
            using (FileStream tmpStream = File.OpenWrite(tempBmpPath))
            {
                srcStream.CopyTo(tmpStream);
            }

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize the content editor and bind it to the loaded document.
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(pdfDoc);

                // Replace the first image (index = 1) on the first page (pageNumber = 1)
                // with the high‑resolution BMP stored in the temporary file.
                editor.ReplaceImage(pageNumber: 1, index: 1, imageFile: tempBmpPath);

                // Save the modified PDF.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdfPath}'.");
        }
        finally
        {
            // Clean up the temporary BMP file.
            if (File.Exists(tempBmpPath))
            {
                try { File.Delete(tempBmpPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}