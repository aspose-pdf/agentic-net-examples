using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string stampImage = "signature_stamp.png"; // predefined image for appearance

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

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the signature field will be placed (first page in this example)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle for the signature field (left, bottom, width, height)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create the signature field with the specified name and rectangle
            SignatureField sigField = new SignatureField(page, fieldRect)
            {
                Name = "ClientSignature"
            };

            // Add the signature field to the page's annotations collection
            page.Annotations.Add(sigField);

            // Create an image stamp from the predefined image
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Position the stamp to match the signature field rectangle
                // XIndent/YIndent are measured from the bottom‑left corner of the page
                XIndent = fieldRect.LLX,
                YIndent = fieldRect.LLY,
                // Set the stamp size to the field size
                Width  = fieldRect.URX - fieldRect.LLX,
                Height = fieldRect.URY - fieldRect.LLY,
                // Ensure the stamp is drawn on top of the field background
                Background = false,
                Opacity = 1.0f
            };

            // Add the image stamp to the same page – this serves as the visual appearance of the signature field
            page.AddStamp(imgStamp);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature field 'ClientSignature' added with image appearance. Saved to '{outputPdf}'.");
    }
}