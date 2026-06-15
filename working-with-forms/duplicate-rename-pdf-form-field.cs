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
        const string originalFieldName = "TextField1"; // name of the field to duplicate
        const int copyCount = 5;                       // how many copies to create

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

            // Page where the original field is placed (0‑based indexing in WidgetAnnotation)
            int pageNumber = originalField.PageIndex;

            // Duplicate the field multiple times
            for (int i = 1; i <= copyCount; i++)
            {
                // New partial name for the copy
                string newPartialName = $"{originalFieldName}_Copy{i}";

                // Add a copy of the original field to the same page with a new name
                // The Add method returns the newly created Field instance.
                Field copy = form.Add(originalField, newPartialName, pageNumber);

                // Optional: shift each copy vertically to avoid overlap
                copy.Rect = new Aspose.Pdf.Rectangle(
                    originalField.Rect.LLX,
                    originalField.Rect.LLY - i * 20, // move down 20 points per copy
                    originalField.Rect.URX,
                    originalField.Rect.URY - i * 20);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}
