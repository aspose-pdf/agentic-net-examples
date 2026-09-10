using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF (will be created if missing)
        const string outputPdfPath = "output.pdf";        // final PDF with stamp
        const string imagePath = "logo.png";              // image to attach and use as stamp
        const string tempPdfPath = "temp_with_attachment.pdf";

        // -----------------------------------------------------------------
        // Ensure required files exist (self‑contained example)
        // -----------------------------------------------------------------
        // 1. Create a minimal placeholder PDF if it does not exist.
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // 2. Create a simple PNG image if it does not exist.
        if (!File.Exists(imagePath))
        {
            using var bmp = new Bitmap(100, 100);
            using var gfx = Graphics.FromImage(bmp);
            gfx.Clear(System.Drawing.Color.Red); // fully‑qualified to avoid ambiguity
            bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Step 1: Attach the image file to the PDF (no visual annotation)
        // -----------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            // Load the source PDF
            contentEditor.BindPdf(inputPdfPath);

            // Add the image as a document attachment with a description
            contentEditor.AddDocumentAttachment(imagePath, "Embedded image attachment");

            // Save the PDF (with attachment) to a temporary file for the next step
            contentEditor.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Add a stamp annotation that references the same image file
        // -----------------------------------------------------------------
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the PDF that already contains the attachment
            fileStamp.BindPdf(tempPdfPath);

            // Create a stamp object (fully‑qualified to avoid ambiguous "Stamp")
            var stamp = new Aspose.Pdf.Facades.Stamp();

            // Bind the image file to the stamp (the same file that was attached)
            stamp.BindImage(imagePath);

            // Position and size of the stamp on the page
            stamp.SetOrigin(100, 500);      // X,Y coordinates of the lower‑left corner
            stamp.SetImageSize(100, 100);   // Width, Height

            // Visual properties
            stamp.Opacity = 0.8f;           // Semi‑transparent
            stamp.IsBackground = true;      // Render behind page content

            // Add the stamp to the PDF (first page by default)
            fileStamp.AddStamp(stamp);

            // Save the final PDF
            fileStamp.Save(outputPdfPath);
        }

        // Optional: clean up the temporary file
        try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"PDF created: {outputPdfPath}");
    }
}
