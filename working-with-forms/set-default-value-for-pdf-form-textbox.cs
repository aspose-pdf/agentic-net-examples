using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // required for DefaultAppearance
using System.Drawing; // for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_default.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the page
            TextBoxField txtField = new TextBoxField(page, rect);

            // Set a default appearance (font, size, color) using the correct constructor
            // Note: DefaultAppearance constructor expects System.Drawing.Color for the color argument
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);
            txtField.DefaultAppearance = appearance;

            // Define the default value that will be shown when the PDF is opened
            txtField.Value = "Enter your name";

            // Optionally set a field name (used for form data extraction)
            txtField.PartialName = "NameField";

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}