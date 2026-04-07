using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // Border, BorderStyle, Dash
using Aspose.Pdf.Forms;        // TextBoxField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "OptionalField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name (cast to a specific field type if needed)
            var field = doc.Form[fieldName] as TextBoxField;
            if (field != null)
            {
                // Create a Border object for the field (requires the parent annotation)
                Border border = new Border(field);

                // Set the border style to dashed
                border.Style = BorderStyle.Dashed;

                // Define dash pattern: 3 units on, 2 units off (optional, can be omitted)
                border.Dash = new Dash(3, 2);

                // Assign the configured border back to the field
                field.Border = border;
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed border on field '{fieldName}' to '{outputPath}'.");
    }
}