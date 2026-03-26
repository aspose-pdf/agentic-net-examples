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
        const string outputJson = "field_names.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            List<string> textFieldNames = new List<string>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field is TextBoxField)
                {
                    // FullName provides the complete field name (including hierarchy)
                    textFieldNames.Add(field.FullName);
                }
            }

            string json = JsonSerializer.Serialize(textFieldNames, new JsonSerializerOptions { WriteIndented = true });
            using (FileStream fs = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }

        Console.WriteLine($"Text field names exported to '{outputJson}'.");
    }
}