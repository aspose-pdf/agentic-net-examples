using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "spellcheck_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field on the page
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "MyTextField", // Field identifier
                Value = string.Empty,        // Initial value
                SpellCheck = true            // Enable spell checking for this field
            };

            // Add the field to the document's form collection on page 1 (1‑based indexing)
            doc.Form.Add(textField, 1);

            // Save the PDF with the spell‑checked field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with spell‑checked text field saved to '{outputPath}'.");
    }
}