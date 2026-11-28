using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath  = "JsonInput.pdf";
        const string jsonPath = "FieldsOutput.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable; using ensures proper closure
        using (Form form = new Form())
        {
            // Load the PDF document containing form fields
            form.BindPdf(pdfPath);

            // Export all form field values to a JSON file using the Stream overload
            using (FileStream jsonStream = File.Create(jsonPath))
            {
                // The second parameter (true) formats the JSON with indentation – optional
                form.ExportJson(jsonStream, true);
            }
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}
