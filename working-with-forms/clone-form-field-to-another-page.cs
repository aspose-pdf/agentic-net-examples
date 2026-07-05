using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // PDF containing the original form field
        const string outputPath = "output.pdf";    // Resulting PDF with the cloned field
        const string originalFieldName = "OriginalField"; // Name of the field to clone
        const int targetPageNumber = 2;            // Page where the clone will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field to be cloned (by its full name)
            // The Form indexer returns a WidgetAnnotation, so we need an explicit cast to Field.
            Field originalField = form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' not found or is not a form field.");
                return;
            }

            // Clone the field onto the target page.
            // Form.Add returns a WidgetAnnotation; cast it back to Field to work with field‑specific members.
            const string clonedPartialName = "ClonedField";
            WidgetAnnotation clonedWidget = form.Add(originalField, clonedPartialName, targetPageNumber);
            Field clonedField = clonedWidget as Field;
            if (clonedField == null)
            {
                Console.Error.WriteLine("Cloned annotation could not be cast to a form field.");
                return;
            }

            // Modify properties of the cloned field as needed.
            // Example: change its rectangle (position and size) on the target page.
            clonedField.Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Example: set a default value for the cloned field.
            clonedField.Value = "Cloned value";

            // Example: change the visual appearance (border color, fill color, etc.).
            clonedField.Color = Aspose.Pdf.Color.Blue; // Border color
            clonedField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Example: make the cloned field read‑only.
            clonedField.ReadOnly = true;

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}
