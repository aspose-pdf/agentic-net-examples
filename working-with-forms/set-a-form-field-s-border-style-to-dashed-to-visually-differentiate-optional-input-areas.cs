using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Border, BorderStyle, WidgetAnnotation
using Aspose.Pdf.Forms;      // Form fields (Field, TextBoxField, etc.)

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
            // Retrieve the widget annotation that represents the form field.
            // The Form indexer returns a WidgetAnnotation, so we cast safely using 'as'.
            WidgetAnnotation? widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Set a dashed border to visually indicate an optional field.
            widget.Border = new Border(widget)
            {
                Style = BorderStyle.Dashed, // Dashed border style
                Width = 1                    // Border width in points (int required)
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed border on field '{fieldName}' to '{outputPath}'.");
    }
}
