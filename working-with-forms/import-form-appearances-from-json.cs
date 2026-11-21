using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border, BorderStyle, DefaultAppearance, AnnotationFlags

class AppearanceSetting
{
    public required string FieldName { get; set; }
    public required string FontName { get; set; }
    public required double FontSize { get; set; }
    public required string TextColorHex { get; set; }      // e.g. "#FF0000"
    public required string BorderColorHex { get; set; }
    public required double BorderWidth { get; set; }
    public required string BackgroundColorHex { get; set; }
}

class Program
{
    // Convert a hex color string ("#RRGGBB") to System.Drawing.Color
    static System.Drawing.Color HexToDrawingColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Hex color string cannot be null or empty.");

        hex = hex.TrimStart('#');
        if (hex.Length != 6)
            throw new ArgumentException("Invalid hex color format. Expected 6 characters.");

        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        return System.Drawing.Color.FromArgb(r, g, b);
    }

    static void Main()
    {
        const string pdfPath = "target.pdf";
        const string jsonPath = "appearance.json";
        const string outputPath = "target_appearance_applied.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Deserialize JSON into a list of settings
        List<AppearanceSetting> settings;
        try
        {
            string json = File.ReadAllText(jsonPath);
            settings = JsonSerializer.Deserialize<List<AppearanceSetting>>(json);
            if (settings == null)
                throw new Exception("Deserialized settings are null.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON: {ex.Message}");
            return;
        }

        using (Document doc = new Document(pdfPath))
        {
            if (doc.Form == null)
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            foreach (AppearanceSetting setting in settings)
            {
                // Retrieve the widget annotation and cast to Field
                Field field = doc.Form[setting.FieldName] as Field;
                if (field == null)
                {
                    Console.WriteLine($"Field '{setting.FieldName}' not found or not a form field; skipping.");
                    continue;
                }

                if (field is TextBoxField textBox)
                {
                    // ----- Font & Text Appearance -----
                    if (!string.IsNullOrWhiteSpace(setting.FontName) && setting.FontSize > 0)
                    {
                        // DefaultAppearance constructor takes font name, size and System.Drawing.Color
                        System.Drawing.Color textColor = string.IsNullOrWhiteSpace(setting.TextColorHex)
                            ? System.Drawing.Color.Black
                            : HexToDrawingColor(setting.TextColorHex);

                        textBox.DefaultAppearance = new DefaultAppearance(
                            setting.FontName,
                            setting.FontSize,
                            textColor);
                    }
                    else if (!string.IsNullOrWhiteSpace(setting.TextColorHex))
                    {
                        // Only colour change, keep existing font settings
                        textBox.DefaultAppearance.TextColor = HexToDrawingColor(setting.TextColorHex);
                    }

                    // ----- Border -----
                    if (!string.IsNullOrWhiteSpace(setting.BorderColorHex) || setting.BorderWidth >= 0)
                    {
                        // Border colour (Characteristics.Border) uses System.Drawing.Color
                        if (!string.IsNullOrWhiteSpace(setting.BorderColorHex))
                            textBox.Characteristics.Border = HexToDrawingColor(setting.BorderColorHex);

                        // Border width & style need a Border object linked to the field
                        if (setting.BorderWidth >= 0)
                        {
                            // Aspose expects integer width; we round the double value
                            int width = (int)Math.Round(setting.BorderWidth);
                            textBox.Border = new Border(textBox)
                            {
                                Width = width,
                                Style = BorderStyle.Solid // solid is the most common; could be parameterised
                            };
                        }
                    }

                    // ----- Background -----
                    if (!string.IsNullOrWhiteSpace(setting.BackgroundColorHex))
                    {
                        textBox.Characteristics.Background = HexToDrawingColor(setting.BackgroundColorHex);
                    }
                }
                else
                {
                    Console.WriteLine($"Field '{setting.FieldName}' is not a TextBoxField; appearance not applied.");
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Appearance settings applied and saved to '{outputPath}'.");
    }
}
