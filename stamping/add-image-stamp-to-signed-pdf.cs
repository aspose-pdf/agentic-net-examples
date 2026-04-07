using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for annotation types if needed (not used here)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string signedPdfPath   = "signed_input.pdf";
        const string stampImagePath  = "stamp.png";
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
            // Load the already signed PDF. No load options are required for a normal PDF.
            using (Document doc = new Document(signedPdfPath))
            {
                // Create an ImageStamp from the image file.
                ImageStamp imgStamp = new ImageStamp(stampImagePath);

                // Configure stamp appearance – optional settings.
                imgStamp.Background = false;                     // place stamp on top of page content
                imgStamp.Opacity    = 0.5;                       // semi‑transparent
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Center;

                // Add the stamp to the first page (page indexing is 1‑based).
                Page firstPage = doc.Pages[1];
                firstPage.AddStamp(imgStamp);

                // Save the document using the default Save() overload.
                // This performs an incremental update, preserving existing digital signatures.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image stamp added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}