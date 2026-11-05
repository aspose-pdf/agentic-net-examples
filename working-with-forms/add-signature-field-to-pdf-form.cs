using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
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

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature field will appear.
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the first page.
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                // The field name (must be unique within the form).
                PartialName = "Signature1",
                // Optional: make the field required.
                Required = true
            };

            // Add the signature field to the form on page 1.
            doc.Form.Add(sigField, 1);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}