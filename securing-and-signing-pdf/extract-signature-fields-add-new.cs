using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form (creates one if absent)
            Form form = doc.Form;

            // Extract and list all existing signature fields
            Console.WriteLine("Existing signature fields:");
            foreach (Field field in form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // Field.Name may be null; use PartialName as fallback
                    string name = sigField.Name ?? sigField.PartialName ?? "(unnamed)";
                    Console.WriteLine($"- {name} on page {sigField.PageIndex}");
                }
            }

            // Define rectangle for the new signature field (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 50);

            // Create a new empty signature field on page 1
            SignatureField newSig = new SignatureField(doc, rect);
            // Optionally set a name for the field
            newSig.Name = "NewSignature";

            // Add the field to the form on page 1 (page numbers are 1‑based)
            form.Add(newSig, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}