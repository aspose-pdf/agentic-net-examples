using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class AddImageStampToSignedPdf
{
    static void Main()
    {
        // Input signed PDF, image to be used as stamp, and output PDF path
        const string signedPdfPath   = "signed_input.pdf";
        const string stampImagePath  = "stamp_logo.png";
        const string outputPdfPath   = "signed_with_stamp.pdf";

        // Verify input files exist
        if (!File.Exists(signedPdfPath))
        {
            Console.Error.WriteLine($"Signed PDF not found: {signedPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Load the already signed PDF (no load options required for PDF)
            using (Document pdfDoc = new Document(signedPdfPath))
            {
                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(stampImagePath);

                // Configure stamp appearance (optional)
                imgStamp.Opacity = 0.5;                 // semi‑transparent
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                imgStamp.BottomMargin = 20;             // distance from bottom edge

                // Add the stamp to the first page (pages are 1‑based)
                pdfDoc.Pages[1].AddStamp(imgStamp);

                // Save the document. In recent Aspose.Pdf versions the default
                // behaviour preserves existing byte‑range signatures when the
                // document is saved without specifying a different save mode.
                // If an older version is used where explicit incremental update
                // is required, the AppendMode property can be set on PdfSaveOptions.
                PdfSaveOptions saveOpts = new PdfSaveOptions();
                // saveOpts.AppendMode = true; // Uncomment if using a version that supports AppendMode
                pdfDoc.Save(outputPdfPath, saveOpts);
            }

            Console.WriteLine($"Image stamp added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
