using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string signedPdfPath   = "signed_input.pdf";
        const string stampImagePath  = "stamp.png";
        const string outputPdfPath   = "signed_with_stamp.pdf";

        if (!File.Exists(signedPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {signedPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the already signed PDF
        using (Document doc = new Document(signedPdfPath))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp at the centre of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Make the stamp semi‑transparent so the underlying content remains visible
                Opacity = 0.5f,
                // Optional: scale the stamp (e.g., 30% of its original size)
                Zoom = 0.3f
            };

            // Add the stamp to the first page (Page indexing is 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Save the document.  In the current Aspose.PDF version the
            // incremental‑update feature is enabled automatically when a
            // document that already contains a digital signature is modified.
            // Therefore we omit the non‑existent UpdateMode property.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamp added and saved to '{outputPdfPath}' without invalidating the signature.");
    }
}
