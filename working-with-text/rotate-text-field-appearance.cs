using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_field.pdf";
        const string fieldName = "MyTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object of the document
            Form form = doc.Form;

            // Create a new text box field (or retrieve an existing one)
            // For simplicity we create a new field here
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField field = new TextBoxField(doc, rect)
            {
                PartialName = fieldName,
                Value = "Sample Text"
            };

            // Add the field to the form
            form.Add(field);

            // Add the initial appearance of the field on page 1
            form.AddFieldAppearance(field, 1, rect);

            // Rotate the field's rectangle by 45 degrees.
            // This rotates the appearance of the text inside the field.
            field.Rect.Rotate(45);

            // Update the appearance after rotation
            form.AddFieldAppearance(field, field.PageIndex + 1, field.Rect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated field: {outputPath}");
    }
}