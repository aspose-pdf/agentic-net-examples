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
        const string outputPath = "output.pdf";
        const string fieldName = "MyFormField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm collection
            Form form = doc.Form;

            // Retrieve the specific field by its fully qualified name.
            // In Aspose.PDF the collection returns a WidgetAnnotation for form fields.
            WidgetAnnotation widget = form[fieldName] as WidgetAnnotation;

            if (widget == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a widget annotation.");
                return;
            }

            // Ensure the field is visible (do not set the Hide flag) and make it non‑printable.
            // The NoPrint flag may not be present in older SDK versions; use its numeric value (256) if needed.
            const AnnotationFlags NoPrintFlag = (AnnotationFlags)256; // 0x100
            widget.Flags = widget.Flags | NoPrintFlag; // add NoPrint while preserving existing flags

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' updated. Saved to '{outputPath}'.");
    }
}
