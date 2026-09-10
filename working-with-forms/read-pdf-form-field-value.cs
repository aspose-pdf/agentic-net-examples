using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fieldName = "myField";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field? field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Field \"{fieldName}\" not found in the document.");
                return;
            }

            // Get the field's value and write it to the console.
            string value = field.Value?.ToString() ?? string.Empty;
            Console.WriteLine($"Field \"{fieldName}\" value: {value}");
        }
    }
}
