using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string originalFieldName = "TextBox1";   // name of the field to clone
        const int targetPageNumber = 2;                // page where the clone will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document contains a form and the source field exists
                if (doc.Form == null || !doc.Form.HasField(originalFieldName))
                {
                    Console.Error.WriteLine($"Form field \"{originalFieldName}\" not found.");
                    return;
                }

                // Retrieve the original field (cast from WidgetAnnotation to Field)
                Field originalField = doc.Form[originalFieldName] as Field;
                if (originalField == null)
                {
                    Console.Error.WriteLine($"Unable to cast original field \"{originalFieldName}\" to a form Field.");
                    return;
                }

                // Define a new partial name for the cloned field (must be unique)
                string clonedPartialName = originalFieldName + "_Clone";

                // Clone the field onto the target page. The Add method returns a WidgetAnnotation.
                // After adding, retrieve the newly created field from the form collection.
                WidgetAnnotation widget = doc.Form.Add(originalField, clonedPartialName, targetPageNumber);
                Field clonedField = doc.Form[clonedPartialName] as Field;
                if (clonedField == null)
                {
                    Console.Error.WriteLine($"Failed to retrieve cloned field \"{clonedPartialName}\" after adding.");
                    return;
                }

                // Modify properties of the cloned field
                // Example: change its rectangle (position & size) on the target page
                // Rectangle constructor: (llx, lly, urx, ury)
                clonedField.Rect = new Rectangle(100, 500, 300, 550);

                // Example: set a default value
                clonedField.Value = "Cloned field value";

                // Example: change the border color
                clonedField.Color = Color.Blue;

                // Example: make the field read‑only
                clonedField.ReadOnly = true;

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cloned field saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
