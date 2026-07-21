using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for WidgetAnnotation and Border

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF with form fields
        const string outputPath = "styled_form.pdf";   // result PDF
        const string fieldName = "MyTextField";        // name of the field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Apply corporate style: border color and width
            // Border colour is set on the annotation itself (Color property)
            widget.Color = Aspose.Pdf.Color.Blue;
            // Border object must be created with the parent annotation instance
            widget.Border = new Border(widget) { Width = 2 };

            // Save the modified document (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' styled and saved to '{outputPath}'.");
    }
}
