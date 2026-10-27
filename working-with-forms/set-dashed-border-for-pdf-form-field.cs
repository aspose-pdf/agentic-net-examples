using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation? widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Set a dashed border style (width = 1 point, black colour)
            widget.Border = new Border(widget)
            {
                Width = 1,
                Style = BorderStyle.Dashed
            };
            // Border colour is defined by the annotation's own Color property
            widget.Color = Color.Black;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field border updated and saved to '{outputPath}'.");
    }
}
