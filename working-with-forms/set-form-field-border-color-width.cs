using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form related classes, including Field
using Aspose.Pdf.Annotations;    // Border class for form fields

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_form.pdf";
        const string fieldName = "MyField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name via the Fields collection
            Field field = null;
            foreach (Field f in doc.Form.Fields)
            {
                if (f.Name == fieldName)
                {
                    field = f;
                    break;
                }
            }

            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Set the border color (color is a property of the field itself)
            field.Color = Color.Blue; // corporate border color

            // Set the border width using the Border class (requires the parent field)
            field.Border = new Border(field) { Width = 2 }; // width in points

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field border updated and saved to '{outputPath}'.");
    }
}
