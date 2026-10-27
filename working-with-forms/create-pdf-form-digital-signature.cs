using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_signature.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Add instruction text to the page
            TextFragment instruction = new TextFragment("Please sign below:");
            instruction.Position = new Position(100, 700);
            page.Paragraphs.Add(instruction);

            // Define the rectangle where the signature field will be placed
            Rectangle signatureRect = new Rectangle(100, 600, 300, 650);

            // Create a signature field on page 1 and add it to the document's form collection
            SignatureField sigField = new SignatureField(doc.Pages[1], signatureRect)
            {
                PartialName = "Signature1",
                // Mark the field as required – the property name is "Required" in the core API
                Required = true
                // Visual styling such as background color is not directly exposed via the core API;
                // it can be customized through the field's appearance if needed.
            };

            // Add the field to the form (page index is 1‑based)
            doc.Form.Add(sigField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with signature field saved to '{outputPath}'.");
    }
}
