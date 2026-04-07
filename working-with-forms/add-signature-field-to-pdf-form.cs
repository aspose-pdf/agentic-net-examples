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

        // Load the PDF, add a signature field, and save.
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the signature field (llx, lly, urx, ury).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create the signature field on the document.
            SignatureField sigField = new SignatureField(doc, rect)
            {
                Name = "Signature1",          // Field name (used to identify the field)
                PartialName = "Signature1",   // Partial name (optional, often same as Name)
                AlternateName = "Sign Here", // Tooltip shown in PDF viewers
                Required = true,             // Mark as required (optional)
                ReadOnly = false             // Allow user to sign
            };

            // Add the field to the form.
            doc.Form.Add(sigField);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}