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
        const string originalFieldName = "myField";   // name of the field to clone
        const string clonedFieldName = "myField_clone";
        const int targetPageNumber = 2;               // page where the clone will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF (using the lifecycle rule for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field that will be cloned (cast from WidgetAnnotation to Field)
            Field originalField = doc.Form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' not found or is not a form field.");
                return;
            }

            // Clone the field onto the target page with a new partial name.
            // The Add method returns a WidgetAnnotation; cast it to Field to work with field members.
            Field clonedField = doc.Form.Add(originalField, clonedFieldName, targetPageNumber) as Field;
            if (clonedField == null)
            {
                Console.Error.WriteLine("Failed to clone the field.");
                return;
            }

            // Modify properties of the cloned field
            // Example: change its position and size (fully qualified Rectangle to avoid ambiguity)
            clonedField.Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Example: set a new default value
            clonedField.Value = "Cloned Value";

            // Example: make the cloned field read‑only
            clonedField.ReadOnly = true;

            // Save the modified document (using the lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}
