using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "spellcheck_form.pdf";

        // Document must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a TextBoxField on the page
            TextBoxField txtField = new TextBoxField(page, rect);
            txtField.PartialName = "MyTextField";   // field name
            txtField.MaxLen = 100;                  // optional: limit length
            txtField.SpellCheck = true;             // enable spell checking

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with spell‑checked text field saved to '{outputPath}'.");
    }
}