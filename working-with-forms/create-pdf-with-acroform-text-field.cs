using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add the first page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text field (llx, lly, urx, ury)
            var rect = new Rectangle(100, 600, 300, 630);

            // Create a TextBoxField on the page
            var textField = new TextBoxField(page, rect)
            {
                PartialName = "SampleTextField",   // field name
                Value = "Enter text here",         // default value
                Color = Aspose.Pdf.Color.LightGray // background color
            };

            // Set a visible border – Border requires the parent annotation (the field itself)
            textField.Border = new Border(textField) { Width = 1 };

            // Add the field to the document's form on page 1
            doc.Form.Add(textField, 1);

            // Save the PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}