using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "text_fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Collect only text field names
        List<string> textFieldNames = new List<string>();
        foreach (Field field in pdfDocument.Form.Fields)
        {
            // TextBoxField represents a text form field
            if (field is TextBoxField)
            {
                // PartialName returns the field's name (without hierarchy prefixes)
                textFieldNames.Add(field.PartialName);
            }
        }

        // Serialize the list to a JSON array
        string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJson, json);

        Console.WriteLine($"Exported {textFieldNames.Count} text field names to '{outputJson}'.");
    }
}
