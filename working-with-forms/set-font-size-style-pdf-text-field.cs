using System;
using System.IO;
using System.Drawing;
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
        const string fieldName  = "MyTextField"; // replace with actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name
            TextBoxField textField = doc.Form[fieldName] as TextBoxField;
            if (textField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // Set global font size limits for form fields (static properties)
            Field.MinFontSize = 10; // minimal font size allowed
            Field.MaxFontSize = 20; // maximal font size allowed

            // Create a DefaultAppearance with desired font, size, and color
            // (rule: use constructor, not object initializer)
            DefaultAppearance appearance = new DefaultAppearance(
                "Helvetica",               // font name
                14,                        // font size
                System.Drawing.Color.Blue);   // text color (System.Drawing)

            // Apply the appearance to the text field
            textField.DefaultAppearance = appearance;

            // Optional: for rich text fields, set a CSS‑like style string
            if (textField is RichTextBoxField richField)
            {
                // Example style: Helvetica, 14pt, bold, italic
                richField.Style = "font-family:Helvetica; font-size:14pt; font-weight:bold; font-style:italic;";
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
