using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF containing the form
        const string pdfPath = "input.pdf";

        // Name of the form field to extract
        const string fieldName = "myField";

        // Verify the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field? field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // The Value property holds the field's data for all field types.
            string fieldValue = field.Value?.ToString() ?? string.Empty;

            Console.WriteLine($"Field '{fieldName}' value: {fieldValue}");
        }
    }
}