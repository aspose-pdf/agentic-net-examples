using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputPdf = "output_custom_font.pdf";   // destination PDF
        const string fontPath  = "customfont.ttf";           // external TrueType/OpenType font
        const string fieldName = "myTextField";              // name of the form field to modify

        // Verify required files exist
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

        // Load the PDF inside a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field by its name; cast to TextBoxField (other field types have similar APIs)
            var field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
            }
            else
            {
                // Load the custom font from the external file
                var customFont = FontRepository.OpenFont(fontPath);

                // Apply the custom font to the field's default appearance.
                // DefaultAppearance is read‑only, but its properties (FontName, FontSize, TextColor) are writable.
                field.DefaultAppearance.FontName   = customFont.FontName; // set the font name
                field.DefaultAppearance.FontSize   = 12;                 // set desired size
                // Use System.Drawing.Color for TextColor (DefaultAppearance expects System.Drawing.Color)
                field.DefaultAppearance.TextColor  = System.Drawing.Color.Black;
            }

            // Save the modified PDF (default PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom font to '{outputPdf}'.");
    }
}
