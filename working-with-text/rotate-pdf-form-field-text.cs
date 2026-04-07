using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class RotateFormField
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_field.pdf";

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

            // Define the rectangle where the field will be placed (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new text box field
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                Name = "RotatedTextField",
                Value = "Rotated Text"
            };

            // Add the field to the form
            form.Add(txtField);

            // Add a custom appearance for the field on page 1
            form.AddFieldAppearance(txtField, 1, fieldRect);

            // Retrieve the normal appearance (key "N") and set its rotation using a transformation matrix.
            if (txtField.States.TryGetValue("N", out var appearanceObj) && appearanceObj is XForm xForm)
            {
                // Rotation matrix for 90 degrees clockwise: [0 1 -1 0 0 0]
                // Aspose.Pdf.Drawing.Matrix ctor: Matrix(a, b, c, d, e, f)
                xForm.Matrix = new Matrix(0, 1, -1, 0, 0, 0);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated form field: {outputPath}");
    }
}
