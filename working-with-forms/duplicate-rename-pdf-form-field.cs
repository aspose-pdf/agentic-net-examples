using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for WidgetAnnotation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string originalFieldName = "myField"; // name of the field to duplicate
        const int copies = 5; // how many copies to create

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the original field; the Form indexer returns a WidgetAnnotation,
            // so we need an explicit cast (or 'as') to Aspose.Pdf.Forms.Field.
            Field originalField = form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' not found or is not a form field.");
                return;
            }

            // Page where the original field is placed (1‑based indexing)
            int pageNumber = originalField.PageIndex;

            // Original rectangle – used as a base for positioning copies
            Aspose.Pdf.Rectangle origRect = originalField.Rect;

            for (int i = 1; i <= copies; i++)
            {
                // New partial name for the duplicated field
                string newName = $"{originalFieldName}_{i}";

                // Add a copy of the original field to the same page.
                // This overload creates a copy if the field already belongs to a form.
                WidgetAnnotation copy = form.Add(originalField, newName, pageNumber);

                // Shift each copy down by 20 points to avoid overlap (optional)
                double offsetY = i * 20;
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    origRect.LLX,
                    origRect.LLY - offsetY,
                    origRect.URX,
                    origRect.URY - offsetY);

                // Apply the new rectangle to the copied field
                copy.Rect = newRect;
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}
