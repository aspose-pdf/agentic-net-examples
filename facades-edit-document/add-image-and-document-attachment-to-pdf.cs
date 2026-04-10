using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";
        const string attachmentPath = "attachment.txt";

        // Verify that all required files exist
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
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Add an image to the first page using PdfFileMend
        // ------------------------------------------------------------
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF
            mend.BindPdf(inputPdf);

            // Add the image to page 1.
            // Parameters: (imageStream, pageNumber, left, bottom, right, top)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Example coordinates: place image with lower‑left corner at (100,500)
                // and upper‑right corner at (300,650) on page 1.
                mend.AddImage(imgStream, 1, 100, 500, 300, 650);
            }

            // Save the intermediate result (image added)
            mend.Save(outputPdf);
        }

        // ------------------------------------------------------------
        // Step 2: Add a document attachment (no visual annotation) using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF that already contains the image
            editor.BindPdf(outputPdf);

            // Attach a file with a description. No annotation is created.
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");

            // Persist the final PDF (overwrites the intermediate file)
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPdf}'.");
    }
}