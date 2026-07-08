using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Rectangle

class Program
{
    static void Main()
    {
        const string outputPath = "spellchecked_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a TextBoxField on the page
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                Name = "Comments",
                PartialName = "Comments",
                // Enable spell checking for this field
                SpellCheck = true,
                // Optional settings
                Multiline = true,
                MaxLen = 500,
                // Set default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with spell‑checked text field saved to '{outputPath}'.");
    }
}