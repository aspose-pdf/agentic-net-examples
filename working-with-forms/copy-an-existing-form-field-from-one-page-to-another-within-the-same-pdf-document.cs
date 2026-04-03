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
        const string fieldName = "myTextField"; // name of the existing field
        const int targetPage = 2;               // page where the field will be copied (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the original field by its full name and cast to Field
            Field originalField = doc.Form[fieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Create a copy of the field on the target page.
            // Provide a new partial name to avoid name collisions.
            string copyPartialName = fieldName + "_Copy";
            // Form.Add returns a WidgetAnnotation; cast it back to Field for further manipulation.
            Field copiedField = doc.Form.Add(originalField, copyPartialName, targetPage) as Field;
            if (copiedField == null)
            {
                Console.Error.WriteLine("Failed to create a copy of the field.");
                return;
            }

            // Optionally adjust the position of the copied field.
            // Here we keep the same rectangle as the original field.
            copiedField.Rect = originalField.Rect;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
    }
}
