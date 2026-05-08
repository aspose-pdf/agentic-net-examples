using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for HorizontalAlignment / VerticalAlignment enums

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_input.pdf";   // existing digitally signed PDF
        const string outputPdf = "signed_with_stamp.pdf";
        const string stampImage = "logo.png";          // image to be used as stamp

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the signed PDF. Using a using block ensures proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp. ImageStamp resides in the core Aspose.Pdf namespace.
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Configure stamp appearance. These settings do not alter the existing signature.
            imgStamp.Background = false;                     // place stamp on top of page content
            imgStamp.Opacity    = 0.7f;                       // semi‑transparent
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the desired page(s). Here we add it to the first page.
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            firstPage.AddStamp(imgStamp);

            // Save the document. Save() without explicit SaveOptions performs an incremental
            // update, preserving any existing digital signatures.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}