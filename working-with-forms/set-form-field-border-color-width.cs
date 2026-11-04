using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_form.pdf";
        const string fieldName = "MyTextBox"; // name of the form field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the form field by name; the Form indexer returns a WidgetAnnotation,
                // so we need an explicit cast to Field (or a safe 'as' cast).
                Field? field = doc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a standard field.");
                    return;
                }

                // Apply corporate style: solid border, 2 points width, corporate blue color.
                // Border color is set via the field's Color property; Border itself has no Color property.
                field.Color = Color.FromRgb(0.0, 0.45, 0.8); // corporate blue
                field.Border = new Border(field)
                {
                    Style = BorderStyle.Solid,
                    Width = 2
                };

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form field '{fieldName}' styled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
