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
        const string originalFieldName = "OriginalField";   // name of the field to clone
        const string clonedFieldName = "ClonedField";       // new name for the cloned field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field that will be cloned – the Form indexer returns a WidgetAnnotation,
            // so we need an explicit cast (or 'as') to Aspose.Pdf.Forms.Field.
            Field originalField = form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' not found or is not a form field.");
                return;
            }

            // Determine a different page number for the clone (pages are 1‑based)
            int targetPage = originalField.PageIndex == 0 ? 2 : 1; // PageIndex is zero‑based

            // Clone the field onto the target page with a new partial name.
            // Form.Add creates a copy of the supplied field.
            Field clonedField = form.Add(originalField, clonedFieldName, targetPage);

            // Modify properties of the cloned field
            clonedField.Rect = new Rectangle(100, 500, 300, 550); // position and size on target page
            clonedField.Color = Color.Blue;                     // visual color of the field
            clonedField.Value = "Cloned value";                // default value
            clonedField.ReadOnly = false;                       // make it editable

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}
