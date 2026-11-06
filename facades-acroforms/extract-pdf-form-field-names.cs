using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: FormFieldsExtractor <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        // Use the Form facade (not FormEditor) to read form field names.
        // Form implements IDisposable, so wrap it in a using block.
        using (Form form = new Form())
        {
            // Bind the PDF document to the facade.
            form.BindPdf(pdfPath);

            // Retrieve all form field names via the FieldNames property.
            string[] fieldNames = form.FieldNames;

            // Serialize the field names to JSON (indented for readability).
            string json = JsonSerializer.Serialize(fieldNames, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Output JSON to the console.
            Console.WriteLine(json);
        }
    }
}
