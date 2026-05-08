using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextBox";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field.
            // The Form collection returns a WidgetAnnotation; cast it to the base type.
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Create a Border instance that is linked to the widget annotation.
            // Border requires the parent annotation in its constructor.
            widget.Border = new Border(widget)
            {
                Width = 1,
                Style = BorderStyle.Beveled // three‑dimensional beveled appearance
            };

            // Set the border colour – colour is a property of the annotation itself, not the Border object.
            widget.Color = Aspose.Pdf.Color.Black;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with beveled border to '{outputPath}'.");
    }
}
