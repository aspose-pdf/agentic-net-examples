using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";          // source PDF (can be empty)
        const string outputPdf = "custom_font_form.pdf";  // result PDF
        const string fontPath  = "MyCustomFont.ttf";      // external TrueType font

        // Validate required files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Load custom font from external file
            Font customFont = FontRepository.OpenFont(fontPath);
            customFont.IsEmbedded = true; // ensure the font is embedded in the PDF

            // Create a DefaultAppearance that uses the custom font
            // Use the constructor that accepts a Font instance (read‑only Font property is handled internally)
            DefaultAppearance appearance = new DefaultAppearance(customFont, 12, System.Drawing.Color.Black);

            // Define the rectangle where the form field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box form field on page 1
            TextBoxField txtField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                Value = "Sample"
            };

            // Assign the custom appearance to the field
            txtField.DefaultAppearance = appearance;

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Apply the appearance to the specified page and rectangle
            // (Form.AddFieldAppearance is the required API for this operation)
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // Save the modified document (lifecycle rule: use Save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom font form field: '{outputPdf}'.");
    }
}