using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_moved_fields.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        // Use FormEditor with the loaded document.
        using (FormEditor editor = new FormEditor(doc))
        {
            // Iterate over all form fields.
            foreach (Field field in doc.Form.Fields)
            {
                // Current rectangle of the field.
                var rect = field.Rect;

                // Apply offset (10 points right, 15 points up).
                float llx = (float)rect.LLX + 10f;
                float lly = (float)rect.LLY + 15f;
                float urx = (float)rect.URX + 10f;
                float ury = (float)rect.URY + 15f;

                // Move the field to the new position.
                editor.MoveField(field.Name, llx, lly, urx, ury);
            }

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Form fields moved and saved to '{outputPdf}'.");
    }
}