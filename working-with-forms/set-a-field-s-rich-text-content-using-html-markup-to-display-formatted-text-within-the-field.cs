using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the input (optional) and output PDF files
        const string outputPath = "RichTextField.pdf";

        // Create a new PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the Rich Text Box field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the RichTextBoxField on the page
            RichTextBoxField richField = new RichTextBoxField(page, rect)
            {
                // Set a partial name for the field (used to reference it later)
                PartialName = "RichTextField",

                // Use HTML markup to define formatted content.
                // FormattedValue accepts HTML-like tags (e.g., <b>, <i>, <u>, <font>, etc.)
                FormattedValue = "<b>Bold Text</b> <i>Italic Text</i> <u>Underlined</u> <font color='red'>Red Text</font>"
            };

            // Add the field to the document's form collection
            doc.Form.Add(richField);

            // Save the PDF (no explicit SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rich text field saved to '{outputPath}'.");
    }
}