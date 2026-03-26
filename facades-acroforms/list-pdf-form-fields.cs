using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: list-pdf-form-fields <pdf-path>");
            return;
        }

        string pdfPath = args[0];
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            // Enumerate form fields via the Fields collection; collect their names.
            var fieldNames = doc.Form?.Fields?.Select(f => f.Name).ToArray() ?? Array.Empty<string>();

            string json = JsonSerializer.Serialize(fieldNames, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }
    }
}