using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using System.Drawing; // System.Drawing.Color is required for appearance settings
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class AppearanceSetting
{
    public string? FieldName { get; set; }
    public string? FontName { get; set; }
    public double? FontSize { get; set; }
    public int[]? TextColor { get; set; }          // [R,G,B] 0‑255
    public int[]? BackgroundColor { get; set; }    // [R,G,B] 0‑255
    public int[]? BorderColor { get; set; }        // [R,G,B] 0‑255
    public double? BorderWidth { get; set; }
}

class Program
{
    // Convert an int[3] RGB array (0‑255) to System.Drawing.Color (0‑255)
    static System.Drawing.Color? FromRgbArray(int[]? rgb)
    {
        if (rgb == null || rgb.Length != 3)
            return null;
        return System.Drawing.Color.FromArgb(rgb[0], rgb[1], rgb[2]);
    }

    // Helper: set a property via reflection if it exists and is writable
    static void SetPropertyIfExists(object target, string propertyName, object value)
    {
        var prop = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(target, value);
        }
    }

    static void Main()
    {
        const string jsonPath = "appearance.json";
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        // Load appearance settings from JSON
        List<AppearanceSetting>? settings;
        try
        {
            string json = File.ReadAllText(jsonPath);
            settings = JsonSerializer.Deserialize<List<AppearanceSetting>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read JSON: {ex.Message}");
            return;
        }

        // Open the target PDF
        using (Document doc = new Document(inputPdf))
        {
            if (doc.Form != null)
            {
                foreach (Field field in doc.Form)
                {
                    // Resolve field name (FullName preferred, fallback to Name)
                    string fieldName = field.FullName ?? field.Name;

                    // Find matching appearance setting
                    AppearanceSetting? setting = settings?.Find(s => s.FieldName == fieldName);
                    if (setting == null)
                        continue;

                    // ---------- TextBoxField handling ----------
                    if (field is TextBoxField textBox)
                    {
                        // ----- Font, size, text colour (DefaultAppearance) -----
                        if (!string.IsNullOrEmpty(setting.FontName) || setting.FontSize.HasValue || setting.TextColor != null)
                        {
                            string fontName = setting.FontName ?? "Helvetica";
                            double fontSize = setting.FontSize ?? 12;
                            System.Drawing.Color txtColor = FromRgbArray(setting.TextColor) ?? System.Drawing.Color.Black;
                            var da = new DefaultAppearance(fontName, fontSize, txtColor);
                            SetPropertyIfExists(textBox, "DefaultAppearance", da);
                        }

                        // ----- Background colour -----
                        System.Drawing.Color? bgColor = FromRgbArray(setting.BackgroundColor);
                        if (bgColor != null)
                        {
                            SetPropertyIfExists(textBox, "BackColor", bgColor.Value);
                            SetPropertyIfExists(textBox, "BackgroundColor", bgColor.Value);
                        }

                        // ----- Border colour & width -----
                        System.Drawing.Color? brColor = FromRgbArray(setting.BorderColor);
                        if (brColor != null)
                        {
                            SetPropertyIfExists(textBox, "BorderColor", brColor.Value);
                        }
                        if (setting.BorderWidth.HasValue)
                        {
                            SetPropertyIfExists(textBox, "BorderWidth", setting.BorderWidth.Value);
                        }

                        // Fallback for border when explicit BorderColor/BorderWidth properties are missing
                        if (brColor != null || setting.BorderWidth.HasValue)
                        {
                            var borderProp = textBox.GetType().GetProperty("Border");
                            if (borderProp != null)
                            {
                                var border = borderProp.GetValue(textBox);
                                if (border == null)
                                {
                                    var borderCtor = typeof(Border).GetConstructor(Type.EmptyTypes);
                                    if (borderCtor != null)
                                    {
                                        border = borderCtor.Invoke(null);
                                        SetPropertyIfExists(textBox, "Border", border);
                                    }
                                }
                                if (border != null)
                                {
                                    if (brColor != null)
                                        SetPropertyIfExists(border, "Color", brColor.Value);
                                    if (setting.BorderWidth.HasValue)
                                        SetPropertyIfExists(border, "Width", setting.BorderWidth.Value);
                                }
                            }
                        }
                    }
                    // ---------- CheckboxField handling (example) ----------
                    else if (field is CheckboxField checkBox)
                    {
                        System.Drawing.Color? bgColor = FromRgbArray(setting.BackgroundColor);
                        if (bgColor != null)
                        {
                            SetPropertyIfExists(checkBox, "BackColor", bgColor.Value);
                            SetPropertyIfExists(checkBox, "BackgroundColor", bgColor.Value);
                        }

                        System.Drawing.Color? brColor = FromRgbArray(setting.BorderColor);
                        if (brColor != null)
                        {
                            SetPropertyIfExists(checkBox, "BorderColor", brColor.Value);
                        }
                        if (setting.BorderWidth.HasValue)
                        {
                            SetPropertyIfExists(checkBox, "BorderWidth", setting.BorderWidth.Value);
                        }
                    }
                    // Additional field types (RadioButtonField, ListBoxField, etc.) can be handled similarly
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with applied appearance: {outputPdf}");
    }
}
