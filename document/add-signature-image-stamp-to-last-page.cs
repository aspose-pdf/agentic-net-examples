using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string signatureImgPath = "signature.png";

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

            // Create an ImageStamp from the signature image file
            using (FileStream imgStream = File.OpenRead(signatureImgPath))
            {
                ImageStamp signatureStamp = new ImageStamp(imgStream);

                // Configure stamp appearance
                signatureStamp.Background = false;          // place over page content
                signatureStamp.Opacity    = 0.85;           // semi‑transparent
                signatureStamp.XIndent    = 100;            // distance from left edge (points)
                signatureStamp.YIndent    = 150;            // distance from bottom edge (points)

                // Optionally set explicit size (in points)
                // signatureStamp.Width  = 200;
                // signatureStamp.Height = 80;

                // Add the stamp to the last page
                lastPage.AddStamp(signatureStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signature image added and saved to '{outputPdfPath}'.");
    }
}