using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Extract all existing signature fields
            // -----------------------------------------------------------------
            var signatureFields = doc.Form?.Fields?.OfType<SignatureField>() ?? Enumerable.Empty<SignatureField>();

            foreach (SignatureField sig in signatureFields)
            {
                // PartialName is the field's identifier
                Console.WriteLine($"Found signature field: {sig.PartialName}");
            }

            // -----------------------------------------------------------------
            // Add a new empty signature field for future signing
            // -----------------------------------------------------------------
            int pageNumber = 1; // 1‑based page index
            Rectangle rect = new Rectangle(100, 500, 200, 550);
            string newFieldName = "Signature_New";

            // Create the signature field and add it to the form
            SignatureField newSigField = new SignatureField(doc.Pages[pageNumber], rect)
            {
                PartialName = newFieldName
            };
            doc.Form.Add(newSigField, pageNumber);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
