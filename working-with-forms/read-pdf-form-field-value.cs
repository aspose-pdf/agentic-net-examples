using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the form field by its name as a Field object (not WidgetAnnotation)
                Field field = doc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.WriteLine($"Form field '{fieldName}' not found.");
                }
                else
                {
                    // Use the Field.Value property to get the field's content safely
                    string value = field.Value?.ToString() ?? string.Empty;
                    Console.WriteLine($"Value of field '{fieldName}': {value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
