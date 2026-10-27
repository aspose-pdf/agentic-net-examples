using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF with form fields
        const string outputPath = "styled_form.pdf";   // result PDF
        const string fieldName = "myField";            // name of the field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name; cast to the appropriate field type
            // (TextBoxField is used as an example – adjust the cast for other field types)
            if (doc.Form[fieldName] is TextBoxField field)
            {
                // Set border width – Border.Width expects an integer value
                field.Border = new Border(field) { Width = 2 };

                // Set border colour – colour is controlled by the field's own Color property
                field.Color = Color.FromRgb(0, 0, 255); // corporate blue

                // Optional: set other visual properties, e.g., background colour
                // field.BackgroundColor = Color.FromRgb(255, 255, 255);
            }
            else
            {
                Console.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}
