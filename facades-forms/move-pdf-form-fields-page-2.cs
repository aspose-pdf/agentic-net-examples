using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document so we can inspect its form fields.
        Document doc = new Document(inputPath);

        // FormEditor works on a Document (or file/stream) and provides the MoveField method.
        using (FormEditor editor = new FormEditor(doc))
        {
            // Iterate over each form field in the document.
            foreach (var field in doc.Form.Fields)
            {
                // PageIndex is zero‑based. Move only fields that reside on page 2 (index 1).
                if (field.PageIndex == 1)
                {
                    // Current rectangle of the field.
                    var rect = field.Rect;
                    float llx = (float)rect.LLX + 5f; // shift right by 5 points
                    float lly = (float)rect.LLY;
                    float urx = (float)rect.URX + 5f;
                    float ury = (float)rect.URY;

                    // Re‑position the field using FormEditor.MoveField.
                    editor.MoveField(field.Name, llx, lly, urx, ury);
                }
            }

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Fields on page 2 moved right by 5 points. Saved to '{outputPath}'.");
    }
}
