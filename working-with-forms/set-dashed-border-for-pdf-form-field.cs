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
        const string outputPath = "output.pdf";
        const string fieldName = "OptionalField"; // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name. The Form indexer returns a WidgetAnnotation, so cast it to Field.
            Field field = doc.Form[fieldName] as Field;
            if (field is TextBoxField textField)
            {
                // The TextBoxField is also a WidgetAnnotation, so we can work with it directly.
                WidgetAnnotation widget = (WidgetAnnotation)textField;

                // Ensure a Border object exists; create one if necessary.
                if (widget.Border == null)
                {
                    widget.Border = new Border(widget);
                }

                // Set the border style to dashed.
                widget.Border.Style = BorderStyle.Dashed;

                // Optional: define dash pattern (e.g., 3 units on, 2 units off) and width.
                widget.Border.Dash = new Dash(3, 2);
                widget.Border.Width = 1; // 1 point width (int)

                Console.WriteLine($"Border style for field '{fieldName}' set to dashed.");
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
