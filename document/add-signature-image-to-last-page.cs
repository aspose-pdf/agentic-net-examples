using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // result PDF
        const string signatureImagePath = "signature.png"; // custom signature image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(signatureImagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImagePath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Determine the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Create an ImageStamp with the custom signature image
            ImageStamp sigStamp = new ImageStamp(signatureImagePath);

            // Position the stamp – set coordinates relative to the page origin (bottom‑left)
            // Example: place the signature 100 points from the left and 150 points from the bottom
            sigStamp.XIndent = 100; // horizontal offset from left edge
            sigStamp.YIndent = 150; // vertical offset from bottom edge

            // Optionally set size (width/height) – if not set, the image keeps its original dimensions
            sigStamp.Width  = 200; // desired width in points
            sigStamp.Height = 100; // desired height in points

            // Ensure the stamp appears on top of existing content
            sigStamp.Background = false;

            // Add the stamp to the last page
            lastPage.AddStamp(sigStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature appearance added and saved to '{outputPdf}'.");
    }
}