using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class AddSignatureField
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_signature.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create the signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            sigField.Name = "UserSignature";               // field identifier
            sigField.Color = Aspose.Pdf.Color.LightGray;    // background color

            // Set the default appearance (font, size, text color)
            sigField.DefaultAppearance = new DefaultAppearance(
                "Helvetica",               // Font name
                12,                        // Font size
                System.Drawing.Color.Black // Text color (System.Drawing.Color required)
            );

            // Border must be assigned after the field instance exists (cannot be set inside the initializer)
            sigField.Border = new Border(sigField) { Width = 1 };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}