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

        try
        {
            // Initialize the Form facade with the PDF file
            using (Form form = new Form(pdfPath))
            {
                // Retrieve all field names
                string[] fieldNames = form.FieldNames;

                // Serialize the array to JSON (indented for readability)
                string json = JsonSerializer.Serialize(fieldNames, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Output JSON to console
                Console.WriteLine(json);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}