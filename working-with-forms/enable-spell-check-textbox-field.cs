using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "spellcheck_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a TextBoxField on the page
            TextBoxField textField = new TextBoxField(page, rect);

            // Set a name for the field (used for form data)
            textField.PartialName = "Comments";

            // Enable spell checking for this field
            textField.SpellCheck = true;

            // Optional: make the field multiline and limit its length
            textField.Multiline = true;
            textField.MaxLen = 500;

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with spell‑checked text field saved to '{outputPath}'.");
    }
}