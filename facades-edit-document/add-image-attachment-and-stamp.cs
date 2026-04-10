using System;
using System.IO;
using System.Drawing; // needed for Bitmap generation and System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";                 // source PDF (will be created if missing)
        const string imageAttachmentPath = "image.jpg";         // image to attach and stamp (will be created if missing)
        const string tempPdfPath = "temp_with_attachment.pdf"; // intermediate file
        const string outputPdfPath = "output.pdf";              // final result

        // ------------------------------------------------------------
        // 1. Ensure prerequisite files exist (create a simple PDF & image)
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // Create a minimal PDF with a single blank page
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdfPath);
            }
        }

        if (!File.Exists(imageAttachmentPath))
        {
            // Generate a simple 100x100 red square JPEG image
            using (Bitmap bmp = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Red);
                }
                bmp.Save(imageAttachmentPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        // ------------------------------------------------------------
        // 2. Add the image as a document attachment (no visual annotation)
        // ------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPdfPath); // load the source PDF
            // Attach the image file; description is optional
            contentEditor.AddDocumentAttachment(imageAttachmentPath, "Embedded image attachment");
            // Save the PDF that now contains the attachment
            contentEditor.Save(tempPdfPath);
        }

        // ------------------------------------------------------------
        // 3. Add a stamp annotation that references the same image file
        // ------------------------------------------------------------
        // Create a stamp object and bind the image (the same file that was attached)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imageAttachmentPath);          // use the image file as stamp content
        stamp.SetOrigin(140, 400);                    // position of the stamp on the page
        stamp.SetImageSize(50, 50);                   // size of the stamp
        stamp.Opacity = 0.8f;                         // semi‑transparent
        stamp.IsBackground = true;                    // place behind page content

        // Initialize PdfFileStamp, bind the PDF that already has the attachment,
        // add the stamp, and save the final document.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(tempPdfPath);                // load the intermediate PDF
            fileStamp.AddStamp(stamp);                     // add the stamp annotation
            fileStamp.Save(outputPdfPath);                 // write the result
        }

        // Optional cleanup of the intermediate file
        if (File.Exists(tempPdfPath))
        {
            File.Delete(tempPdfPath);
        }

        Console.WriteLine($"PDF with attachment and stamp saved to '{outputPdfPath}'.");
    }
}
