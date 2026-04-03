using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_duplicated.pdf";
        const string originalFieldName = "TextField1"; // name of the field to duplicate
        const int copies = 5;                         // how many copies to create
        const int targetPage = 1;                     // page where copies will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the source field exists
            if (!doc.Form.HasField(originalFieldName))
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' not found.");
                return;
            }

            // Retrieve the original field instance (cast from WidgetAnnotation to Field)
            Field originalField = doc.Form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Unable to cast field '{originalFieldName}' to a form Field.");
                return;
            }

            // Create the requested number of copies
            for (int i = 1; i <= copies; i++)
            {
                // Build a unique name for each copy
                string newPartialName = $"{originalFieldName}_copy{i}";

                // Add a copy of the original field to the form on the target page.
                // The Add method returns a WidgetAnnotation; cast it back to Field.
                WidgetAnnotation widget = doc.Form.Add(originalField, newPartialName, targetPage);
                Field newField = widget as Field;
                if (newField == null)
                {
                    // If the cast fails, skip this iteration.
                    continue;
                }

                // OPTIONAL: adjust the position of each copy to avoid overlap.
                // Here we shift each copy 20 points downwards.
                Aspose.Pdf.Rectangle rect = newField.Rect;
                rect.LLY -= 20 * i;
                rect.URY -= 20 * i;
                newField.Rect = rect;
            }

            // Save the modified PDF (PDF format is the default when no SaveOptions are supplied)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}
