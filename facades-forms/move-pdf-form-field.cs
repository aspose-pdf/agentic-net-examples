using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field and cast it to Aspose.Pdf.Forms.Field
            Field? field = doc.Form["DateField"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Field 'DateField' not found or is not a form field.");
                return;
            }

            // Define the new rectangle: lower‑left (100,200), upper‑right (250,220)
            var newRect = new Rectangle(100, 200, 250, 220); // width = 150, height = 20

            // Apply the new rectangle to the field
            field.Rect = newRect;

            // Move the field to page 2 – use AddFieldAppearance because Field has no PageNumber property
            doc.Form.AddFieldAppearance(field, 2, newRect);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field moved and saved to '{outputPath}'.");
    }
}
