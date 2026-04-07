using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";   // existing PDF or blank PDF
        const string outputPdf = "richtext_field.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page.
        if (!File.Exists(inputPdf))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPdf);
            }
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle where the rich‑text field will appear.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 400, 750);

            // Create the RichTextBoxField.
            RichTextBoxField richField = new RichTextBoxField(page, rect)
            {
                // Optional: set a name for the field.
                Name = "RichTextField1",
                // Optional: make the field multiline.
                Multiline = true,
                // Optional: set a default style (font, size, color) for the field.
                Style = "font-family:Helvetica; font-size:12pt; color:#000000;",
                // Set the formatted (HTML) value.
                FormattedValue = "<b>Bold Text</b> and <i>Italic Text</i> with <span style=\"color:#FF0000;\">red color</span>."
            };

            // Add the field to the document's form collection (not directly to page annotations).
            // The second argument is the page number (1‑based).
            doc.Form.Add(richField, 1);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Rich‑text field added and saved to '{outputPdf}'.");
    }
}
