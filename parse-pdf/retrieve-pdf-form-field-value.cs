using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "FieldName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name using the indexer and cast to Field
            Field? field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
            }
            else
            {
                // Output field information
                Console.WriteLine($"Field '{fieldName}' type: {field.GetType().Name}");
                Console.WriteLine($"Field value: {field.Value}");
            }
        }
    }
}