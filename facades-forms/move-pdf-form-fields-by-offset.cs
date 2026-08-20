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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind a FormEditor to the loaded document
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);

                // Offset to apply to each field (10 points right, 15 points up)
                const float offsetX = 10f;
                const float offsetY = 15f;

                // Iterate over all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    // Full name of the field (required by MoveField)
                    string fieldName = field.FullName;

                    // Current rectangle of the field (LLX, LLY, URX, URY are doubles)
                    Aspose.Pdf.Rectangle rect = field.Rect;

                    // Compute new rectangle coordinates with the offset, casting doubles to float
                    float newLlx = (float)rect.LLX + offsetX;
                    float newLly = (float)rect.LLY + offsetY;
                    float newUrx = (float)rect.URX + offsetX;
                    float newUry = (float)rect.URY + offsetY;

                    // Move the field to the new position
                    editor.MoveField(fieldName, newLlx, newLly, newUrx, newUry);
                }

                // Save changes made by the FormEditor back to the document
                // The parameter‑less Save() is obsolete; using the overload that saves to the bound document.
                editor.Save();
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields moved and saved to '{outputPath}'.");
    }
}
