using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the form field
        const string outputPath = "output.pdf";  // destination PDF after copying the field
        const string fieldName  = "myTextField"; // name of the existing field to copy
        const int    targetPage = 2;             // page number (1‑based) where the copy will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the existing field by its name – the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field.
            Field originalField = doc.Form[fieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field in the document.");
                return;
            }

            // Create a new name for the copied field to avoid name collisions
            string copyPartialName = fieldName + "_copy";

            // Add a copy of the field to the target page.
            // Form.Add(Field, string, int) creates a duplicate of the supplied field on the specified page.
            Field copiedField = doc.Form.Add(originalField, copyPartialName, targetPage);

            // Optional: adjust the position of the copied field on the new page.
            // Uncomment and modify the rectangle as needed.
            // copiedField.Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} as '{fieldName}_copy'. Saved to '{outputPath}'.");
    }
}
