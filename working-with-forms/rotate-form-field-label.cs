using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class RotateFormFieldLabel
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_label.pdf";
        const string fieldName = "MyLabel"; // name of the form field to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field – the Form indexer returns a WidgetAnnotation,
            // so we must cast it to Aspose.Pdf.Forms.Field before accessing field members.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a standard field.");
                return;
            }

            // Rotate the field's rectangle by 45 degrees. The rotation affects the visual
            // representation of the field, including its label.
            Aspose.Pdf.Rectangle rect = field.Rect;
            rect.Rotate(45); // 45‑degree rotation
            field.Rect = rect; // apply the rotated rectangle back to the field

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' rotated and saved to '{outputPath}'.");
    }
}
