using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            ExportFieldsToJsonOptions options = new ExportFieldsToJsonOptions();
            options.WriteIndented = true;
            document.Form.ExportToJson(jsonPath, options);
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}