using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // Border, BorderStyle, WidgetAnnotation
using Aspose.Pdf.Forms;        // TextBoxField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "myTextField"; // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field (cast to a specific field type, e.g., TextBoxField)
            TextBoxField field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
            }
            else
            {
                // The form field itself is a WidgetAnnotation, so we can set its border directly.
                // Set the border style to Beveled (three‑dimensional appearance)
                // Border requires the parent annotation in its constructor (rule: border-requires-parent-annotation)
                field.Border = new Border(field) { Style = BorderStyle.Beveled };
            }

            // Save the modified PDF (using rule for disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with beveled border to '{outputPath}'.");
    }
}
