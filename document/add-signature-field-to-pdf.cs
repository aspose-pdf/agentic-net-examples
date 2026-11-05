using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Border, BorderStyle, DefaultAppearance, AnnotationFlags
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the signature field will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the signature field (coordinates in points)
            // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field (no object initializer for decoration members)
            SignatureField sigField = new SignatureField(page, rect);
            sigField.Name = "UserSignature";
            sigField.PartialName = "Signature";

            // Tooltip is represented by AlternateName (read‑only, set via property)
            sigField.AlternateName = "Please sign here";

            // Appearance settings to match document style
            // Background and border colours are set via Characteristics (System.Drawing.Color)
            sigField.Characteristics.Background = System.Drawing.Color.LightGray;
            sigField.Characteristics.Border = System.Drawing.Color.Black;

            // Border width and style are defined with a Border object that needs the field as parent
            sigField.Border = new Border(sigField) { Width = 1, Style = BorderStyle.Solid };

            // Placeholder text appearance (DefaultAppearance) – font, size, colour
            sigField.DefaultAppearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Blue);

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}
