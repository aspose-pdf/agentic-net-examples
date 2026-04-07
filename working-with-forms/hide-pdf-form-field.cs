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
        const string outputPath = "output_hidden_field.pdf";
        const string fieldName = "MyTextField"; // name of the form field to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form contains the specified field
            if (!doc.Form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Form field '{fieldName}' does not exist.");
                return;
            }

            // Retrieve the widget annotation that represents the form field
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a widget annotation and cannot be hidden.");
                return;
            }

            // Combine the Hidden flag with any existing flags (preserve other flags)
            widget.Flags |= AnnotationFlags.Hidden;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' hidden and saved to '{outputPath}'.");
    }
}
