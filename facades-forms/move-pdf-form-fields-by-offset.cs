using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_moved.pdf";
        const float offsetX = 10f;
        const float offsetY = 15f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create FormEditor with the document instance (non‑obsolete constructor)
        using (FormEditor editor = new FormEditor(doc))
        {
            // Iterate over each form field in the document
            foreach (var field in doc.Form.Fields)
            {
                // Current rectangle of the field (coordinates are double)
                var rect = field.Rect;

                // Apply the offset – cast to float because MoveField expects float values
                float newLlX = (float)rect.LLX + offsetX;
                float newLlY = (float)rect.LLY + offsetY;
                float newUrX = (float)rect.URX + offsetX;
                float newUrY = (float)rect.URY + offsetY;

                // Move the field to the new position
                editor.MoveField(field.Name, newLlX, newLlY, newUrX, newUrY);
            }

            // Persist the changes to the output PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"All form fields have been moved and saved to '{outputPath}'.");
    }
}
