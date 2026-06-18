using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for color definitions if needed

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
            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect);

            // Set the default appearance (font, size, color) to match the document style
            // DefaultAppearance constructor accepts font name, size and Aspose.Pdf.Color
            sigField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Optional: set border and background color to blend with the page
            sigField.Border = new Border(sigField) { Width = 1 };
            sigField.Color = Aspose.Pdf.Color.LightGray;

            // Add the signature field to the form on page 1 (pages are 1‑based)
            doc.Form.Add(sigField, 1);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}