using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // WidgetAnnotation, Border, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // existing PDF with a form
        const string outputPath = "styled_form.pdf";   // result PDF
        const string fieldName = "myField";            // name of the field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Form field \"{fieldName}\" not found or is not a widget annotation.");
                return;
            }

            // Set the border width via a Border instance that references the widget.
            widget.Border = new Border(widget) { Width = 2 }; // corporate border width (points)

            // Border colour is defined by the annotation's own Color property (Border has no Color).
            widget.Color = Aspose.Pdf.Color.Blue; // corporate border colour

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field \"{fieldName}\" border updated and saved to '{outputPath}'.");
    }
}
