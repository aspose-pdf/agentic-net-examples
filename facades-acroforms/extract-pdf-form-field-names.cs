using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the PDF file
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: FormFieldsExtractor <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found - {pdfPath}");
            return;
        }

        // Open the PDF form using Aspose.Pdf.Facades.Form
        // The Form class implements IDisposable, so we wrap it in a using block
        using (Form form = new Form(pdfPath))
        {
            // Retrieve all field names from the form
            string[] fieldNames = form.FieldNames;

            // Serialize the field names array to JSON (indented for readability)
            string json = JsonSerializer.Serialize(fieldNames, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Output the JSON to standard output
            Console.WriteLine(json);
        }
    }
}