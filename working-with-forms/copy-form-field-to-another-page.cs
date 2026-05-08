using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF containing the form field
        const string outputPath = "output.pdf";         // PDF with the copied field
        const string fieldName  = "myField";            // name of the field to copy
        const int    targetPage = 2;                    // page number (1‑based) where the copy will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the field exists.
            if (!doc.Form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found in the document.");
                return;
            }

            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field originalField = doc.Form[fieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" could not be cast to a form Field.");
                return;
            }

            // Copy the field to the target page.
            // Provide a new partial name for the copied field to avoid name collisions.
            string copiedPartialName = fieldName + "_Copy";
            Field copiedField = doc.Form.Add(originalField, copiedPartialName, targetPage);

            // Optionally, adjust the position of the copied field on the target page.
            // Here we simply offset it by 50 points to the right and down.
            Aspose.Pdf.Rectangle originalRect = originalField.Rect;
            copiedField.Rect = new Aspose.Pdf.Rectangle(
                originalRect.LLX + 50,
                originalRect.LLY - 50,
                originalRect.URX + 50,
                originalRect.URY - 50);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" copied to page {targetPage} and saved as \"{outputPath}\".");
    }
}
