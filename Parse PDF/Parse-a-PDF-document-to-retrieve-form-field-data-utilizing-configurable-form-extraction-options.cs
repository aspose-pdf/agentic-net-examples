using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Retrieve form fields via the Form.Fields collection.
            // -----------------------------------------------------------------
            Console.WriteLine("Form fields (name = value):");
            foreach (Field field in doc.Form.Fields)
            {
                // Some fields may not have a value; handle null safely.
                string value = field.Value?.ToString() ?? "<empty>";
                Console.WriteLine($"{field.FullName} = {value}");
            }

            // -----------------------------------------------------------------
            // 2. Export form fields to JSON using configurable export options.
            // -----------------------------------------------------------------
            // Create export options – you can adjust properties as needed.
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions();
            // Example configuration (uncomment if the property exists):
            // jsonOptions.IncludeEmptyFields = true;

            // Export to a memory stream to avoid writing a temporary file.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                doc.Form.ExportToJson(jsonStream, jsonOptions);
                jsonStream.Position = 0; // Reset stream position for reading.

                using (StreamReader reader = new StreamReader(jsonStream))
                {
                    string jsonOutput = reader.ReadToEnd();
                    Console.WriteLine("\nExported JSON:");
                    Console.WriteLine(jsonOutput);
                }
            }

            // -----------------------------------------------------------------
            // (Optional) If you need to save the document after any modifications,
            // use the Save method with explicit SaveOptions if the target format
            // is not PDF. Here we simply save back to PDF.
            // -----------------------------------------------------------------
            // doc.Save("output.pdf"); // Uncomment if saving is required.
        }
    }
}