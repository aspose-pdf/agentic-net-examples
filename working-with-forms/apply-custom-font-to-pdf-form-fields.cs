using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // FontRepository, FontTypes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom_font.pdf";
        const string fontPath = @"C:\WINDOWS\Fonts\arial.ttf"; // path to custom TTF font

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Load the custom font from file
            Font customFont;
            using (FileStream fs = File.OpenRead(fontPath))
            {
                customFont = FontRepository.OpenFont(fs, FontTypes.TTF);
            }
            // Ensure the font will be embedded in the output PDF
            customFont.IsEmbedded = true;

            // Loop through all form fields and set the custom font
            foreach (Field field in doc.Form.Fields)
            {
                // Apply the custom font (size 12, black color) to each field
                field.DefaultAppearance = new DefaultAppearance(customFont.FontName, 12, System.Drawing.Color.Black);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom font applied to all form fields: {outputPath}");
    }
}