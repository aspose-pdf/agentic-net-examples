using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "spellcheck_demo.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box form field
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "SampleTextBox",
                Value = "Enter text here"
            };

            // Aspose.Pdf does not expose a spell‑checking API for form fields.
            // If such a feature existed, it might look like:
            // txtField.SpellCheck = true;
            // Since it is not available, no action is taken here.

            // Add the text box field to the document's form collection
            doc.Form.Add(txtField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'. Spell checking is not supported by Aspose.Pdf.");
    }
}