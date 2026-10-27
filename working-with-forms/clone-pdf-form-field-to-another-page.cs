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
        const string sourceFieldName = "TextField1"; // name of the field to clone
        const int targetPageNumber = 2; // page where the clone will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the original form field and cast it to Field
                Field sourceField = doc.Form[sourceFieldName] as Field;
                if (sourceField == null)
                {
                    Console.Error.WriteLine($"Form field '{sourceFieldName}' not found or is not a form field.");
                    return;
                }

                // Clone the field (deep copy). The Clone method returns a WidgetAnnotation, so cast it back to Field.
                Field clonedField = sourceField.Clone() as Field;
                if (clonedField == null)
                {
                    Console.Error.WriteLine("Failed to clone the form field.");
                    return;
                }

                // Modify properties of the cloned field
                clonedField.PartialName = "Cloned_" + sourceFieldName; // give it a unique name
                clonedField.Value = "Cloned value"; // set a new default value

                // Define the rectangle where the cloned field will appear
                Rectangle rect = new Rectangle(100, 500, 200, 550);
                clonedField.Rect = rect;

                // Add the cloned field to the form collection
                doc.Form.Add(clonedField);

                // Place the cloned field on the target page using AddFieldAppearance
                doc.Form.AddFieldAppearance(clonedField, targetPageNumber, rect);

                // Save the updated PDF
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
