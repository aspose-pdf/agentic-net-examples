using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class RotateFormFieldText
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_field.pdf";
        const string fieldName = "MyTextField"; // replace with your field name
        const int rotationAngle = 90; // degrees (0‑360)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation. Cast it safely to the concrete field type.
            TextBoxField textField = doc.Form[fieldName] as TextBoxField;
            if (textField == null)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found or is not a TextBoxField.");
                return;
            }

            // Rotate the rectangle that defines the field's visual area.
            // This changes the appearance of the field (including its text).
            textField.Rect.Rotate(rotationAngle);

            // After changing the rectangle, rebuild the appearance stream for the widget.
            // Page numbers in Aspose.Pdf are 1‑based.
            int pageNumber = textField.PageIndex + 1;
            doc.Form.AddFieldAppearance(textField, pageNumber, textField.Rect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated field saved to \"{outputPath}\".");
    }
}
