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
        const string outputPath = "output_hidden.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the form contains the specified field
            if (!doc.Form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // Retrieve the field as a WidgetAnnotation (the visual representation of a form field)
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;

            // Hide the field by setting the Hidden flag on the annotation
            if (widget != null)
            {
                widget.Flags |= AnnotationFlags.Hidden;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a widget annotation.");
                return;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden field to '{outputPath}'.");
    }
}
