using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_moved.pdf";
        const float offsetX = 10f; // horizontal offset
        const float offsetY = 15f; // vertical offset

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Use FormEditor that works with a Document instance (no destination in ctor)
        using (FormEditor editor = new FormEditor(doc))
        {
            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Current rectangle of the field (coordinates are double)
                var rect = field.Rect;

                // Apply the offset – cast to float as required by MoveField
                float llx = (float)rect.LLX + offsetX;
                float lly = (float)rect.LLY + offsetY;
                float urx = (float)rect.URX + offsetX;
                float ury = (float)rect.URY + offsetY;

                // Move the field to the new position
                editor.MoveField(field.Name, llx, lly, urx, ury);
            }

            // Save the modified PDF to the desired output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"All form fields have been moved and saved to '{outputPath}'.");
    }
}
