using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form object exists
            Form form = doc.Form;

            // Set a default appearance for all form fields (font, size, color)
            // DefaultAppearance.Font is read‑only; use the constructor that accepts font name, size and color.
            form.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Define the rectangle where the signature field will be placed (llx, lly, urx, ury)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                Name = "UserSignature",               // field name
                AlternateName = "Sign Here",          // tooltip shown in Acrobat
                Color = Aspose.Pdf.Color.LightGray   // border color (optional)
            };

            // Add the signature field to the form on page 1
            form.Add(sigField, 1);

            // Add an additional appearance for the field (optional, matches document style)
            // This places another visual representation of the field on the same page.
            form.AddFieldAppearance(sigField, 1, sigRect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}