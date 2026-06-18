using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "logo.png";
        const string attachmentDescription = "Logo image attachment";
        const string tempPdf = "temp_with_attachment.pdf"; // intermediate file with attachment
        const string finalPdf = "output.pdf";              // final result with stamp

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Add the image file as a document attachment (no annotation)
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdf);

            // Add the image as an attachment with a description
            editor.AddDocumentAttachment(imagePath, attachmentDescription);

            // Save the PDF that now contains the attachment
            editor.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // Step 2: Add a stamp annotation that references the same image
        // ------------------------------------------------------------
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the PDF that already has the attachment
            fileStamp.BindPdf(tempPdf);

            // Create a stamp object and bind the image file to it
            Stamp stamp = new Stamp();
            stamp.BindImage(imagePath);               // reference the image file
            stamp.SetOrigin(100, 700);                // position on the page (x, y)
            stamp.SetImageSize(100, 100);             // width and height of the stamp
            stamp.Opacity = 0.7f;                     // semi‑transparent
            stamp.PageNumber = 1;                     // target page (1‑based)

            // Add the stamp to the document (page is defined via stamp.PageNumber)
            fileStamp.AddStamp(stamp);

            // Save the final PDF with the stamp annotation
            fileStamp.Save(finalPdf);
        }

        Console.WriteLine($"Stamp added and saved to {finalPdf}");
    }
}
