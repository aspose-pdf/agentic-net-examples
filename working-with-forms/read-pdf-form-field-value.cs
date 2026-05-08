using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fieldName = "MyFieldName";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Extract the field's value and write it to the console
            string value = field.Value?.ToString() ?? string.Empty;
            Console.WriteLine($"Field '{fieldName}' value: {value}");
        }
    }
}
