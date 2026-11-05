using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "form_fields.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Verify that the PDF contains form fields
                if (doc.Form == null || doc.Form.Fields.Count() == 0)
                {
                    Console.WriteLine("No form fields found in the document.");
                    return;
                }

                using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
                {
                    // CSV header
                    writer.WriteLine("Name,PartialName,FullName,Type,Value,Rect");

                    foreach (Field field in doc.Form.Fields)
                    {
                        string name        = EscapeCsv(field.Name);
                        string partialName = EscapeCsv(field.PartialName);
                        string fullName    = EscapeCsv(field.FullName);
                        string typeName    = EscapeCsv(field.GetType().Name);
                        string value       = EscapeCsv(field.Value?.ToString() ?? string.Empty);
                        string rect        = EscapeCsv(field.Rect != null ? field.Rect.ToString() : string.Empty);

                        writer.WriteLine($"{name},{partialName},{fullName},{typeName},{value},{rect}");
                    }
                }

                Console.WriteLine($"Form fields exported to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Escapes commas, quotes and line breaks for CSV compliance
    static string EscapeCsv(string input)
    {
        if (input.Contains("\""))
            input = input.Replace("\"", "\"\"");

        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
            return $"\"{input}\"";

        return input;
    }
}