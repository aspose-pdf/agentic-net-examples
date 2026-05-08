using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "MyField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the AcroForm object from the document
            Form acroForm = doc.Form;

            try
            {
                // Retrieve the form field by its name using the indexer.
                // The Form indexer returns a WidgetAnnotation, so we need to cast it to Field.
                Field? field = acroForm[fieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                    return;
                }

                // Output basic information about the retrieved field
                Console.WriteLine($"Field '{fieldName}' found. Type: {field.GetType().Name}");
                Console.WriteLine($"Current value: {field.Value}");
            }
            catch (Exception ex)
            {
                // The indexer throws if the field is not present
                Console.Error.WriteLine($"Error retrieving field '{fieldName}': {ex.Message}");
            }
        }
    }
}
