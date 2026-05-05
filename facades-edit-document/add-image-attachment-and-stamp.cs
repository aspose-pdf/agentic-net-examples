using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, image to attach and use as stamp, and output PDF paths
        const string inputPdfPath   = "input.pdf";
        const string imagePath      = "logo.png";
        const string intermediatePdfPath = "temp_stamp.pdf";
        const string outputPdfPath  = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add a stamp that uses the image (via BindImage)
        // ------------------------------------------------------------
        // PdfFileStamp is a facade for stamping; it implements IDisposable.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPdfPath);

            // Create a stamp and bind the image file
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imagePath);                     // Image used as stamp
            stamp.SetOrigin(100, 500);                     // Position of the stamp (lower‑left corner)
            stamp.SetImageSize(150, 150);                  // Width and height of the stamp
            stamp.Opacity = 0.7f;                          // Semi‑transparent
            stamp.IsBackground = false;                    // Draw over page content

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save to an intermediate file (still without attachment)
            fileStamp.Save(intermediatePdfPath);
            fileStamp.Close(); // Required for facades
        }

        // ------------------------------------------------------------
        // 2. Attach the same image file to the PDF (no visual annotation)
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF that already contains the stamp
            editor.BindPdf(intermediatePdfPath);

            // Add the image as a document attachment (no visible annotation)
            editor.AddDocumentAttachment(imagePath, "Embedded image attachment");

            // Save the final PDF with both stamp and attachment
            editor.Save(outputPdfPath);
            editor.Close(); // Required for facades
        }

        Console.WriteLine($"PDF created with stamp and attachment: {outputPdfPath}");
    }
}