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
        const string outputPath = "output_with_new_sig.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Extract existing signature fields
            // -----------------------------------------------------------------
            Console.WriteLine("Existing signature fields:");
            foreach (Field field in doc.Form?.Fields ?? Enumerable.Empty<Field>())
            {
                if (field is SignatureField sigField)
                {
                    // PartialName is the field name; Rect gives the location on the page
                    Console.WriteLine($"  Name: {sigField.PartialName}, Rect: {sigField.Rect}");
                }
            }

            // -----------------------------------------------------------------
            // Add a new empty signature field for future signing
            // -----------------------------------------------------------------
            const string baseFieldName = "SignatureField_New";
            string fieldName = baseFieldName;
            int suffix = 1;

            // Ensure the field name is unique within the form
            while (doc.Form?.Fields?.Any(f => f.PartialName == fieldName) == true)
            {
                fieldName = $"{baseFieldName}_{suffix++}";
            }

            // Choose the page (1‑based) and rectangle where the field will appear
            int targetPage = 1; // first page (Aspose.Pdf pages are 1‑based)
            // Rectangle(llx, lly, urx, ury) – coordinates are in points
            Rectangle rect = new Rectangle(100, 100, 250, 150);

            // Create the signature field and add it to the form
            SignatureField newSig = new SignatureField(doc.Pages[targetPage], rect)
            {
                PartialName = fieldName
            };
            // The Form.Add method expects only the field instance; the name is already set via PartialName.
            doc.Form.Add(newSig);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new signature field: {outputPath}");
    }
}
