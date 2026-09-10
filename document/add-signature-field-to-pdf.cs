using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class AddSignatureField
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_signature.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle: using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (left, bottom, right, top)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // Set a logical name for the field (used in form data)
                Name = "UserSignature",
                // Set the default appearance to match the document style
                // Use the constructor overload because DefaultAppearance.Font is read‑only
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the form on page 1
            doc.Form.Add(sigField, 1);

            // Optionally add an additional appearance (ensures the field is visible)
            doc.Form.AddFieldAppearance(sigField, 1, sigRect);

            // Save the modified PDF (lifecycle: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}