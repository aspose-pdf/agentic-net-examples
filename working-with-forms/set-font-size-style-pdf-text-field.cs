using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // needed for DefaultAppearance and Color

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                return;
            }

            // Set the default appearance (font name, size, and color)
            // Use the constructor because DefaultAppearance.Font is read‑only
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 14, System.Drawing.Color.Black);
            txtField.DefaultAppearance = appearance;

            // Optionally enforce minimum/maximum font size checks
            // -1 disables the check; here we allow any size
            Field.MinFontSize = -1;
            Field.MaxFontSize = -1;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Font size and style updated. Saved to '{outputPath}'.");
    }
}