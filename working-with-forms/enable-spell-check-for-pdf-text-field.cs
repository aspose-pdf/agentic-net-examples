using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "spellcheck_form.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a TextBoxField on the page
            TextBoxField textField = new TextBoxField(page, rect)
            {
                PartialName = "MyTextField",   // field name
                Contents    = "Enter text here",
                SpellCheck  = true             // enable spell checking
            };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with spell‑checking field saved to '{outputPath}'.");
    }
}