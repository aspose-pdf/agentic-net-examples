using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle (llx, lly, urx, ury) where the signature field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the document
            SignatureField sigField = new SignatureField(doc, rect)
            {
                PartialName   = "Signature1",          // field name
                AlternateName = "Sign Here",          // tooltip shown in Acrobat
                // Optional: set a visible border color (border color is taken from the annotation itself)
                Color = Aspose.Pdf.Color.Black
            };

            // Border must be created with the parent annotation (the signature field) as a constructor argument
            sigField.Border = new Border(sigField) { Width = 1 };

            // Add the signature field to the form collection of the document
            doc.Form.Add(sigField);

            // Save the modified PDF with the new signature field
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}
