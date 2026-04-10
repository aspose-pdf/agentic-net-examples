using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Correct namespace for form fields

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

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Create FormEditor using the document instance (non‑obsolete constructor)
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Current rectangle (double values)
                var rect = field.Rect;

                // Apply the required offset and cast to float as required by MoveField
                float llx = (float)rect.LLX + 10f;
                float lly = (float)rect.LLY + 15f;
                float urx = (float)rect.URX + 10f;
                float ury = (float)rect.URY + 15f;

                // Move the field to the new position
                formEditor.MoveField(field.Name, llx, lly, urx, ury);
            }

            // Save the modified PDF to the output path
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Form fields moved and saved to '{outputPdf}'.");
    }
}
