using System;
using System.IO;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the form object
            Aspose.Pdf.Forms.Form form = doc.Form;

            if (form == null || form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Verify that the specified field exists
            if (!form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Retrieve the widget annotation representing the field
            Aspose.Pdf.Annotations.WidgetAnnotation widget = form[fieldName] as Aspose.Pdf.Annotations.WidgetAnnotation;

            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a widget annotation.");
                return;
            }

            // Mark the field as required; PDF viewers will show a red border when left empty after submission
            widget.Required = true;

            // For XFA forms, enable drawing of red rectangles around required fields
            form.EmulateRequierdGroups = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with required field '{fieldName}' set. Output: {outputPath}");
    }
}