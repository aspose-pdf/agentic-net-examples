using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string signatureImgPath = "signature.png";   // custom signature image
        const string outputPdfPath = "output_signed.pdf"; // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(signatureImgPath))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImgPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Create an ImageStamp with the custom signature image
            ImageStamp signatureStamp = new ImageStamp(signatureImgPath);

            // Configure stamp appearance and position
            signatureStamp.Background = false;                     // draw on top of page content
            signatureStamp.Opacity = 0.9;                         // slightly transparent if desired
            signatureStamp.HorizontalAlignment = HorizontalAlignment.Center;
            signatureStamp.VerticalAlignment = VerticalAlignment.Bottom;

            // Set explicit coordinates (optional – overrides alignment if set)
            signatureStamp.XIndent = 100;   // distance from left edge
            signatureStamp.YIndent = 50;    // distance from bottom edge
            signatureStamp.Width = 150;    // stamp width
            signatureStamp.Height = 50;    // stamp height

            // Add the stamp to the last page
            lastPage.AddStamp(signatureStamp);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signature appearance added and saved to '{outputPdfPath}'.");
    }
}
