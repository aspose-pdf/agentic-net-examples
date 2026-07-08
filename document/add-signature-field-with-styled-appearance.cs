using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using System.Drawing; // System.Drawing.Color required for DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the document
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.Name = "UserSignature";
            sigField.AlternateName = "Signature";

            // Set the field's default appearance to match the document style
            // DefaultAppearance(string fontName, double fontSize, System.Drawing.Color textColor)
            sigField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based page indexing)
            doc.Form.Add(sigField, 1);

            // Optionally add an additional appearance (same rectangle) for consistency
            doc.Form.AddFieldAppearance(sigField, 1, rect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}