using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_appearance.pdf";
        const string signatureImagePath = "signature.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(signatureImagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Determine the last page (Aspose.Pdf uses 1‑based indexing)
            int lastPageNumber = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageNumber];

            // Create an image stamp with the custom signature image
            ImageStamp signatureStamp = new ImageStamp(signatureImagePath);

            // Optional: set the size of the stamp (width and height in points)
            signatureStamp.Width = 150;   // example width
            signatureStamp.Height = 50;   // example height

            // Position the stamp on the page (coordinates are measured from the bottom‑left corner)
            signatureStamp.XIndent = 100; // distance from the left edge
            signatureStamp.YIndent = 100; // distance from the bottom edge

            // Set opacity if a translucent appearance is desired
            signatureStamp.Opacity = 0.8f;

            // Add the stamp to the last page
            lastPage.AddStamp(signatureStamp);

            // Save the updated PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with signature appearance saved to '{outputPdfPath}'.");
    }
}