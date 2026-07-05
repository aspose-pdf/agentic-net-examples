using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for WidgetAnnotation type

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";   // existing PDF with a form field
        const string outputPath = "output_form.pdf"; // PDF after duplication
        const string sourceFieldName = "TextField1"; // name of the field to duplicate
        const int duplicateCount = 5;                 // how many copies to create

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field that will be duplicated – the Form indexer returns a WidgetAnnotation,
            // so we must cast it to Aspose.Pdf.Forms.Field explicitly.
            Field originalField = form[sourceFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{sourceFieldName}' not found or is not a form field.");
                return;
            }

            // originalField.PageIndex is 1‑based (same as doc.Pages indexing)
            int pageNumber = originalField.PageIndex;

            for (int i = 1; i <= duplicateCount; i++)
            {
                // Create a new unique partial name for the copy
                string newPartialName = $"{sourceFieldName}_{i}";

                // Form.Add returns a WidgetAnnotation that represents the visual widget of the field.
                // Cast it back to Field to work with field‑specific members.
                WidgetAnnotation widget = form.Add(originalField, newPartialName, pageNumber);
                Field copiedField = widget as Field;
                if (copiedField == null)
                {
                    // If the cast fails, skip this iteration – this should not happen for standard fields.
                    continue;
                }

                // Optionally adjust the position of each copy to avoid overlap.
                // Here we shift each copy 20 points downwards.
                copiedField.Rect = new Rectangle(
                    originalField.Rect.LLX,
                    originalField.Rect.LLY - i * 20,
                    originalField.Rect.URX,
                    originalField.Rect.URY - i * 20);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated field saved to '{outputPath}'.");
    }
}
