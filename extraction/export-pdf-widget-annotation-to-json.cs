using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
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
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                Field field = doc.Form[widgetName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Form field with name '{widgetName}' not found or is not a form field.");
                    return;
                }

                // Field derives from WidgetAnnotation, so we can treat it as such to get the rectangle.
                WidgetAnnotation widget = field;

                var widgetInfo = new
                {
                    Name = field.PartialName,
                    Rect = widget != null ? new
                    {
                        LLX = widget.Rect.LLX,
                        LLY = widget.Rect.LLY,
                        URX = widget.Rect.URX,
                        URY = widget.Rect.URY
                    } : null,
                    Value = GetFieldValue(field)
                };

                string json = JsonSerializer.Serialize(widgetInfo, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, json);
                Console.WriteLine($"Form field exported to '{jsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to retrieve the value of common form field types
    static object GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked;
            case RadioButtonField rad:
                // Return the selected option's name if any
                if (rad.Selected > 0 && rad.Options != null && rad.Options.Count >= rad.Selected)
                    return rad.Options[rad.Selected - 1].Name;
                return null;
            case ListBoxField list:
                return list.SelectedItems; // returns int[] of selected indices (1‑based)
            case ComboBoxField combo:
                return combo.Value;
            default:
                return null;
        }
    }
}
