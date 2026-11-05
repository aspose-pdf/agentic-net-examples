using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for DefaultAppearance
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom_font.pdf";
        const string fontPath = "MyCustomFont.ttf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Load the custom TrueType font (fully qualified to avoid ambiguity)
            Aspose.Pdf.Text.Font customFont = FontRepository.OpenFont(fontPath);

            // Loop through all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Apply the font only to text box fields
                if (field is TextBoxField textBox)
                {
                    // Set the default appearance of the field to use the custom font.
                    // DefaultAppearance constructor expects System.Drawing.Color, not Aspose.Pdf.Color.
                    textBox.DefaultAppearance = new DefaultAppearance(customFont.FontName, 12, System.Drawing.Color.Black);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom font to '{outputPath}'.");
    }
}
