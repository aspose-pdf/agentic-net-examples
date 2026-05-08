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
        const string outputPath = "hidden_field.pdf";
        const string fieldName = "MyTextField"; // name of the form field to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Verify that the field exists
            if (form.HasField(fieldName))
            {
                // The visual representation of a form field is a WidgetAnnotation.
                // Retrieve it via a safe cast.
                WidgetAnnotation? widget = form[fieldName] as WidgetAnnotation;
                if (widget != null)
                {
                    // Hide the widget (field) on the page.
                    widget.Flags = AnnotationFlags.Hidden;
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' does not have a widget annotation that can be hidden.");
                }
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden field to '{outputPath}'.");
    }
}
