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
        const string outputPath = "styled_output.pdf";
        const string fieldName = "CorporateField"; // name of the field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the target field; the indexer returns an Annotation, so cast it to WidgetAnnotation
            // (the base class for form fields) to gain access to border and color properties.
            WidgetAnnotation widget = form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Set the border width via the Border object (requires the parent annotation).
            widget.Border = new Border(widget) { Width = 2 };

            // Set the border (and visual) color directly on the annotation.
            // The Border class does not expose a Color property; the annotation's Color property
            // controls the border color for form fields.
            widget.Color = Aspose.Pdf.Color.FromRgb(0, 0, 255); // corporate blue

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' styled and saved to '{outputPath}'.");
    }
}
