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
        const string fieldName = "myField";      // name of the existing field
        const int targetPage = 2;                // page number to copy the field to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the form object
                Form form = doc.Form;

                // Retrieve the field that will be copied – the Form indexer returns a WidgetAnnotation,
                // so we must cast it to Aspose.Pdf.Forms.Field.
                Field sourceField = form[fieldName] as Field;
                if (sourceField == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                    return;
                }

                // Create a copy of the field on the target page with a new partial name.
                // Form.Add returns a WidgetAnnotation; cast it back to Field.
                Field copiedField = form.Add(sourceField, fieldName + "_Copy", targetPage) as Field;
                if (copiedField == null)
                {
                    Console.Error.WriteLine("Failed to create a copy of the field.");
                    return;
                }

                // Optionally reposition the copied field (example: place it at (100,500))
                // Preserve the original width and height.
                Rectangle origRect = sourceField.Rect;
                Rectangle newRect = new Rectangle(
                    100,                                 // lower‑left X
                    500,                                 // lower‑left Y
                    100 + (origRect.URX - origRect.LLX), // upper‑right X
                    500 + (origRect.URY - origRect.LLY)  // upper‑right Y
                );

                // Add the appearance of the copied field on the target page at the new rectangle.
                form.AddFieldAppearance(copiedField, targetPage, newRect);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
