using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the PDF file.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: dotnet run <pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Verify that the file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        // Open the PDF form using Aspose.Pdf.Facades.Form.
        // The Form class implements IDisposable, so we wrap it in a using block.
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the array of field names.
            string[] fieldNames = form.FieldNames;

            // Serialize the field names to JSON with indentation for readability.
            string jsonOutput = JsonSerializer.Serialize(
                fieldNames,
                new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to standard output.
            Console.WriteLine(jsonOutput);
        }
    }
}