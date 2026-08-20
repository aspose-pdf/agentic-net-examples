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
        const string fieldName = "MyField"; // name of the form field to modify

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
                Console.Error.WriteLine($"Field \"{fieldName}\" not found or is not a widget annotation.");
                return;
            }

            // Clear the Print flag so the field is visible on screen but omitted from printing
            widget.Flags = widget.Flags & ~AnnotationFlags.Print;

            // Ensure the field is not hidden (no Invisible or Hidden flags set)
            widget.Flags = widget.Flags & ~AnnotationFlags.Invisible & ~AnnotationFlags.Hidden;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" set to visible‑only (non‑printable). Saved to '{outputPath}'.");
    }
}
