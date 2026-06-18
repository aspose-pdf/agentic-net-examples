using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName = "myTextField";
        const string fieldValue = "Provided string value";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, fill the specified text field, and save the result.
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field safely.
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // For text fields (e.g., TextBoxField) the Value property sets the content.
                field.Value = fieldValue;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
