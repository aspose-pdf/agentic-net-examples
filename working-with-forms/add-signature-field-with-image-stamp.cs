using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and the image that will be used as the signature appearance
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_signed.pdf";
        const string stampImagePath = "signature_stamp.png";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the signature field will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle (left, bottom, width, height) for the signature field
            // Adjust the values to suit the desired position and size
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);

            // -----------------------------------------------------------------
            // 1. Add the image stamp that will serve as the visual appearance
            // -----------------------------------------------------------------
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            // Position the stamp using the same rectangle as the signature field
            imgStamp.XIndent = sigRect.LLX;          // left coordinate
            imgStamp.YIndent = sigRect.LLY;          // bottom coordinate
            imgStamp.Width   = sigRect.URX - sigRect.LLX; // width
            imgStamp.Height  = sigRect.URY - sigRect.LLY; // height
            // Place the stamp behind the page content so the signature field sits on top
            imgStamp.Background = true;
            page.AddStamp(imgStamp);

            // ---------------------------------------------------------------
            // 2. Create the signature field and add it to the page annotations
            // ---------------------------------------------------------------
            SignatureField sigField = new SignatureField(page, sigRect);
            // Set the field name (this is the identifier used when signing)
            sigField.Name = "ClientSignature";
            // Optionally set a tooltip (alternate name) that appears in PDF viewers
            sigField.AlternateName = "Client Signature";

            // Add the signature field to the page's annotation collection
            page.Annotations.Add(sigField);

            // -----------------------------------------------------------------
            // 3. (Optional) If you need to add an additional appearance entry,
            //    you can use the Form.AddFieldAppearance method.
            // -----------------------------------------------------------------
            // Form form = doc.Form;
            // form.AddFieldAppearance(sigField, 1, sigRect);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPdfPath}'.");
    }
}