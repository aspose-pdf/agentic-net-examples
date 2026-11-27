using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string widgetName = "myWidget"; // name of the WidgetAnnotation to export

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            WidgetAnnotation targetWidget = null;
            int targetPageNumber = -1; // will hold the 1‑based page number where the widget resides

            // Pages are 1‑based (see global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is WidgetAnnotation widget && widget.Name == widgetName)
                    {
                        targetWidget = widget;
                        targetPageNumber = i; // capture the page number
                        break;
                    }
                }
                if (targetWidget != null) break;
            }

            if (targetWidget == null)
            {
                Console.Error.WriteLine($"WidgetAnnotation named '{widgetName}' not found.");
                return;
            }

            // Create a DTO for JSON serialization
            var widgetInfo = new
            {
                Name = targetWidget.Name,
                Rect = new
                {
                    LLX = targetWidget.Rect.LLX,
                    LLY = targetWidget.Rect.LLY,
                    URX = targetWidget.Rect.URX,
                    URY = targetWidget.Rect.URY
                },
                // For form fields the value is stored in the Contents property
                Value = targetWidget.Contents,
                Flags = targetWidget.Flags,
                PageNumber = targetPageNumber // use the captured page number instead of a non‑existent property
            };

            // Serialize to JSON using a MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(ms, widgetInfo, options);
                ms.Position = 0; // rewind for reading

                // Example: output JSON string to console
                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine(json);
                }

                // If needed, raw JSON bytes are available via ms.ToArray()
                // byte[] jsonBytes = ms.ToArray();
            }
        }
    }
}
