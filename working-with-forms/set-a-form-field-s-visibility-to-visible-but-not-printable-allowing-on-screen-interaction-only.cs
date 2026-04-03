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
        const string fieldName = "MyField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field. In recent Aspose.PDF versions the collection returns a WidgetAnnotation.
            WidgetAnnotation widget = doc.Form[fieldName] as WidgetAnnotation;
            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Ensure the field is visible (clear the Hidden flag if set)
            widget.Flags &= ~AnnotationFlags.Hidden;

            // Make the field non‑printable: clear the Print flag (there is no NoPrint flag in the current enum)
            widget.Flags &= ~AnnotationFlags.Print;

            // Optionally, make sure the field is not read‑only so the user can interact with it
            widget.ReadOnly = false;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
