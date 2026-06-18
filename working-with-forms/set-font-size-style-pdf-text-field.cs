using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyTextField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name and cast to TextBoxField (or RichTextBoxField as needed)
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // Set global font size limits for all fields (optional, -1 disables the check)
            Field.MinFontSize = 10; // minimal readable size
            Field.MaxFontSize = 24; // maximal size to avoid overflow

            // Create a DefaultAppearance with desired font, size, and color
            // Note: DefaultAppearance.Font is read‑only; use the constructor overload.
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 14, System.Drawing.Color.Blue);
            txtField.DefaultAppearance = appearance; // apply to the field

            // Optionally, set the field's style (e.g., bold/italic) via RichTextBoxField if needed
            // RichTextBoxField richField = txtField as RichTextBoxField;
            // if (richField != null)
            // {
            //     // CSS‑like style string: font-weight:bold; font-style:italic;
            //     richField.Style = "font-weight:bold; font-style:italic;";
            // }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Font size and style updated. Saved to '{outputPath}'.");
    }
}