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

        using (Document doc = new Document(inputPath))
        {
            using (FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Correct API call – Export form fields to JSON
                doc.Form.ExportToJson(fs);
            }
        }

        Console.WriteLine($"Form fields exported to '{jsonPath}'.");
    }
}
