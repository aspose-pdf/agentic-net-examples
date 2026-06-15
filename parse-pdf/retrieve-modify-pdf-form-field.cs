using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";          // Path to the source PDF
        const string fieldName = "CustomerName";      // Name of the form field to retrieve

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name using the Form indexer.
            // The indexer returns a WidgetAnnotation; cast it to Field.
            Field field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Display basic information about the field
            Console.WriteLine($"Field '{fieldName}' found.");
            Console.WriteLine($"Full name: {field.FullName}");
            Console.WriteLine($"Current value: {field.Value}");

            // Example modification: set a new value for the field
            field.Value = "John Doe";

            // Save the updated PDF
            const string outputPath = "form_modified.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}
