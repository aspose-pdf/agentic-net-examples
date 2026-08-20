using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page (page index is 1‑based)
            SignatureField sigField = new SignatureField(doc, rect)
            {
                Name = "Signature1",               // internal field name
                AlternateName = "Sign Here",       // tooltip shown in PDF viewers
                Required = true,                  // make the field required
                ReadOnly = false                  // allow user input
            };

            // Add the signature field to the document's form
            doc.Form.Add(sigField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}