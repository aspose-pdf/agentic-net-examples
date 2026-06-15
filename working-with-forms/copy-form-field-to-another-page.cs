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

        // Name of the existing field to copy and the target page number (1‑based)
        const string sourceFieldName = "MyTextField";
        const int targetPageNumber = 2;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrap Document in using as per rule)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field from the form by its name.
            // The Form indexer returns a WidgetAnnotation, so we cast it to Field.
            Field sourceField = doc.Form[sourceFieldName] as Field;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Field '{sourceFieldName}' not found or is not a form field.");
                return;
            }

            // Create a copy of the field for the target page.
            // The Add method creates a new field instance based on the supplied field.
            string copiedFieldName = sourceFieldName + "_Copy";
            doc.Form.Add(sourceField, copiedFieldName, targetPageNumber);

            // Save the modified document (Document.Save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{sourceFieldName}' copied to page {targetPageNumber} and saved as '{outputPath}'.");
    }
}
