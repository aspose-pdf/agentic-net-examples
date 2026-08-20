using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_field.pdf";
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Verify that the specified field exists
            if (!form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Retrieve the field (assumed to be a text box)
            TextBoxField txtField = (TextBoxField)form[fieldName];

            // Rotate the field rectangle by 90 degrees
            txtField.Rect.Rotate(Rotation.on90);

            // Re‑create the field appearance on its page with the rotated rectangle
            // (no custom appearance object is required for a simple text box)
            form.AddFieldAppearance(txtField, txtField.PageIndex + 1, txtField.Rect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated field saved to '{outputPath}'.");
    }
}
