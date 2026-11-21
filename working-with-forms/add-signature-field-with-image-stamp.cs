using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "signed_output.pdf"; // result PDF
        const string stampImagePath = "signature_stamp.png"; // predefined image for appearance

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the signature field will be placed
            // (llx, lly, urx, ury) – adjust coordinates as needed
            Rectangle rect = new Rectangle(100, 500, 300, 550);

            // Create a signature field on the page
            SignatureField sigField = new SignatureField(page, rect)
            {
                PartialName = "ClientSignature" // field name
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // Set a visual appearance for the field using a predefined image.
            // Aspose.Pdf does not expose an ImageScale property on ImageStamp.
            // Instead we size the stamp to match the field rectangle and place it
            // at the same coordinates using XIndent/YIndent.
            // -----------------------------------------------------------------
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Size the stamp to exactly fit the field rectangle
                Width = rect.URX - rect.LLX,
                Height = rect.URY - rect.LLY,
                // Position the stamp at the lower‑left corner of the rectangle
                XIndent = rect.LLX,
                YIndent = rect.LLY,
                // Ensure the stamp is placed *under* the field widget (Background = true)
                Background = true
            };

            // Apply the stamp to the page – this becomes the visual appearance of the field
            imgStamp.Put(page);

            // -----------------------------------------------------------------
            // (Optional) If you need to explicitly set the appearance dictionary,
            // you can add a custom XForm entry keyed by "N". The ImageStamp itself
            // is not an XForm, so the common approach is to rely on the placed stamp
            // as shown above.
            // -----------------------------------------------------------------

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}
