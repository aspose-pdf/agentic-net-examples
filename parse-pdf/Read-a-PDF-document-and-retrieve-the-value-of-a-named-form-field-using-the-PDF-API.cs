using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // Path to the PDF file
        const string fieldName = "myField";     // Name of the form field to read

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the annotation by its name and cast it to a Field
            Field? field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Get the field's value; Value may be null, so use null‑conditional operator
            string value = field.Value?.ToString() ?? string.Empty;

            Console.WriteLine($"Field '{fieldName}' value: {value}");
        }
    }
}
