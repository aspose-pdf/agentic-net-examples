using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class AddAcroFormFieldExample
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_signature.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a rectangle that defines the position and size of the signature field
            // Aspose.Pdf.Rectangle(left, bottom, width, height) – all values are in points
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 50);

            // Initialize a new signature field on the document
            SignatureField signatureField = new SignatureField(doc, fieldRect)
            {
                // Optional: set a partial name (field identifier) and make it read‑only
                PartialName = "Signature1",
                ReadOnly = true
            };

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based page indexing)
            // Using the overload that also sets the partial name explicitly
            doc.Form.Add(signatureField, "Signature1", 1);

            // Save the modified document (Document.Save handles disposal internally)
            doc.Save(outputPath);
        }

        Console.WriteLine($"AcroForm field added and saved to '{outputPath}'.");
    }
}