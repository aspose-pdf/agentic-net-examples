using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_signed.pdf";   // result PDF
        const string stampImg  = "signature_stamp.png"; // predefined image for appearance

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will be placed (coordinates are in points)
            // left, bottom, right, top
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the document (using the Document‑based constructor)
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Name = "ClientSignature"   // set the field name
            };

            // Add the signature field to the PDF form
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // Set a custom appearance for the signature field using an image.
            // The approach is to create an ImageStamp and associate it with the
            // field via Form.AddFieldAppearance, which adds the appearance to
            // the specified page and rectangle.
            // -----------------------------------------------------------------
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Ensure the stamp is drawn on top of the field content
                Background = false,
                // Optional: adjust opacity, scaling, etc.
                Opacity = 1.0f
            };

            // Place the image stamp on the same page (page 1) at the same rectangle.
            // This effectively becomes the visual representation of the signature field.
            doc.Form.AddFieldAppearance(sigField, 1, sigRect);
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature field 'ClientSignature' added with image appearance. Saved to '{outputPdf}'.");
    }
}