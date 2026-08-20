using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hidden.pdf";
        const string fieldName = "myField";   // name of the form field to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Hide the field on the rendered page by setting the Hidden flag
            widget.Flags |= AnnotationFlags.Hidden;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' hidden and saved to '{outputPath}'.");
    }
}
