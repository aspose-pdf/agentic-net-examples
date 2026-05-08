using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the PDF file.
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

        // Use Aspose.Pdf.Facades.Form to work with AcroForm fields.
        // The Form class implements IDisposable, so wrap it in a using block.
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the array of field names.
            string[] fieldNames = form.FieldNames;

            // Serialize the field names to JSON (indented for readability).
            string json = JsonSerializer.Serialize(fieldNames, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Output the JSON to the console.
            Console.WriteLine(json);
        }
    }
}