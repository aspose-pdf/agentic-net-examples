using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // path to the PDF containing the form
        const string fieldName = "MyField";     // name of the field whose value we want

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document actually contains a form and the requested field exists
            if (doc.Form != null && doc.Form.HasField(fieldName))
            {
                // Retrieve the field via the form indexer (returns a WidgetAnnotation)
                var widget = doc.Form[fieldName];

                // Cast to the concrete Field type to access the Value property
                if (widget is Field field)
                {
                    // The Value property holds the field's current content
                    string value = field.Value?.ToString() ?? string.Empty;
                    Console.WriteLine($"Field \"{fieldName}\" value: {value}");
                }
                else
                {
                    Console.WriteLine($"Field \"{fieldName}\" is not a standard form field.");
                }
            }
            else
            {
                Console.WriteLine($"Field \"{fieldName}\" not found in the document.");
            }
        }
    }
}