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
        const string fieldName = "myField";   // name of the form field to copy
        const int sourcePage = 1;               // original page (1‑based) – kept for logging only
        const int targetPage = 2;               // page to copy the field to

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the existing field (by name) – the Form indexer returns a WidgetAnnotation,
            // so we must cast it to Field explicitly.
            Field sourceField = doc.Form[fieldName] as Field;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a standard field.");
                return;
            }

            // Clone the field to create an independent copy.
            Field copiedField = (Field)sourceField.Clone();

            // Give the copied field a unique name (optional but recommended).
            copiedField.Name = $"{sourceField.Name}_copy_page{targetPage}";

            // Preserve the original rectangle (position and size).
            var rect = new Aspose.Pdf.Rectangle(
                sourceField.Rect.LLX,
                sourceField.Rect.LLY,
                sourceField.Rect.URX,
                sourceField.Rect.URY);
            copiedField.Rect = rect;

            // Add the cloned field to the document's form collection.
            doc.Form.Add(copiedField);

            // Place the field on the target page. AddFieldAppearance creates the visual
            // representation of the field on the specified page.
            doc.Form.AddFieldAppearance(copiedField, targetPage, rect);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied from page {sourcePage} to page {targetPage}.");
    }
}
