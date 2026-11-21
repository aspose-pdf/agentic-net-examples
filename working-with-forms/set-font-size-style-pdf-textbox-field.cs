using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextBox"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form and the specified field exists
            var field = doc.Form[fieldName];
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // Retrieve the text box field (cast from the generic FormField)
            TextBoxField textBox = field as TextBoxField;
            if (textBox == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                return;
            }

            // Choose a readable font, style, and size
            string fontName = "Arial"; // Aspose.Pdf uses font name strings for DefaultAppearance
            double fontSize = 14; // increase size for better readability

            // Apply the font, size, and color (black) to the field's default appearance
            // NOTE: DefaultAppearance expects System.Drawing.Color, so we fully qualify it to avoid ambiguity with Aspose.Pdf.Color.
            textBox.DefaultAppearance = new DefaultAppearance(fontName, fontSize, System.Drawing.Color.Black);

            // Optionally set the current value to demonstrate the new appearance
            textBox.Value = "Sample text with larger bold font";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
