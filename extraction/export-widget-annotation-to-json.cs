using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "widget.json";
        const string widgetName = "myWidget"; // name of the form field to export

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load PDF via FileStream and ensure deterministic disposal
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        using (Document doc = new Document(pdfStream))
        {
            // Locate the form field (WidgetAnnotation) by its name/partial name
            Field targetField = null;
            foreach (Field field in doc.Form.Fields)
            {
                // Some fields expose Name, others expose PartialName – check both
                if (string.Equals(field.Name, widgetName, StringComparison.Ordinal) ||
                    string.Equals(field.PartialName, widgetName, StringComparison.Ordinal))
                {
                    targetField = field;
                    break;
                }
            }

            if (targetField == null)
            {
                Console.Error.WriteLine($"Form field named '{widgetName}' not found.");
                return;
            }

            // Prepare a DTO for JSON serialization
            var widgetInfo = new
            {
                Name = targetField.Name ?? targetField.PartialName,
                Rect = new
                {
                    LLX = targetField.Rect?.LLX ?? 0,
                    LLY = targetField.Rect?.LLY ?? 0,
                    URX = targetField.Rect?.URX ?? 0,
                    URY = targetField.Rect?.URY ?? 0
                },
                Value = targetField.Value?.ToString()
            };

            string json = JsonSerializer.Serialize(widgetInfo, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON using a FileStream
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(jsonStream))
            {
                writer.Write(json);
            }

            Console.WriteLine($"Form field exported to {jsonPath}");
        }
    }
}
