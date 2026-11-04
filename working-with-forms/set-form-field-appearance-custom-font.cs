using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fontPath = "customfont.ttf";
        const string fieldName = "myTextField";

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

        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field; it must be a TextBoxField
            var field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // Load external TrueType/OpenType font (fully qualified to avoid ambiguity)
            Aspose.Pdf.Text.Font customFont = FontRepository.OpenFont(fontPath);

            // Create a DefaultAppearance that uses the custom font
            var appearance = new DefaultAppearance(customFont.FontName, 12, System.Drawing.Color.Black);
            field.DefaultAppearance = appearance;

            // Optional: set a sample value to see the font applied
            field.Value = "Sample text";

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom font applied to field '{fieldName}'.");
    }
}
