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
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the form field by name
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"TextBoxField '{fieldName}' not found.");
                return;
            }

            // Set minimum and maximum font size to enforce readability
            Field.MinFontSize = 10;   // minimal size allowed
            Field.MaxFontSize = 20;   // maximal size allowed

            // Define default appearance: font name, size, and color
            // Use the constructor that accepts (string fontName, double fontSize, Color textColor)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 14, System.Drawing.Color.Black);
            txtField.DefaultAppearance = appearance;

            // Optionally, set the field's style (e.g., bold and italic) via RichTextBoxField if needed
            // Here we demonstrate setting a rich text style string
            if (txtField is RichTextBoxField richField)
            {
                // Example style: bold, italic, underline, font size 14, font name Helvetica
                richField.Style = "font-family:Helvetica; font-size:14pt; font-weight:bold; font-style:italic; text-decoration:underline;";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Font size and style updated. Saved to '{outputPath}'.");
    }
}