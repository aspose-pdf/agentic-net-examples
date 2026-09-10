using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_signed.pdf";
        const string stampImagePath = "signature_stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the signature field will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the signature field (coordinates in points)
            // left, bottom, right, top
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create the signature field on the selected page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                Name = "ClientSignature" // Set the field name
            };

            // Add the signature field to the page annotations collection
            page.Annotations.Add(sigField);

            // Create an image stamp from the predefined image
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp to match the signature field rectangle
                XIndent = sigRect.LLX,
                YIndent = sigRect.LLY,
                Width = sigRect.URX - sigRect.LLX,
                Height = sigRect.URY - sigRect.LLY,
                // Ensure the stamp is drawn on top of the field (default)
                Background = false,
                Opacity = 1.0f
            };

            // Add the image stamp to the same page
            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature field added and appearance set. Saved to '{outputPdf}'.");
    }
}