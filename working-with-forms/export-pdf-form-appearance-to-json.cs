using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace FormFieldAppearanceExport
{
    // DTO for JSON serialization – all reference members are nullable to satisfy the non‑nullable warnings.
    public class FormFieldAppearance
    {
        public string? Name { get; set; }
        public string? FieldType { get; set; }
        public string? FontName { get; set; }
        public double? FontSize { get; set; }
        public string? TextColor { get; set; }
        public string? BorderColor { get; set; }
        public string? BackgroundColor { get; set; }
        public double? BorderWidth { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "template.pdf";
            const string outputJsonPath = "field_appearances.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPdfPath))
                {
                    var form = doc.Form;
                    // Ensure the document actually contains form fields
                    if (form == null || form.Fields == null || !form.Fields.Any())
                    {
                        Console.WriteLine("No form fields found in the document.");
                        return;
                    }

                    var appearances = new List<FormFieldAppearance>();

                    // Iterate over all form fields using the correct collection
                    foreach (var field in form.Fields)
                    {
                        // Text‑related appearance lives in DefaultAppearance (System.Drawing.Color)
                        var txtField = field as TextBoxField;
                        var defaultAppearance = txtField?.DefaultAppearance;

                        var appearance = new FormFieldAppearance
                        {
                            Name = field.FullName,
                            FieldType = field.GetType().Name,
                            FontName = defaultAppearance?.Font?.FontName,
                            FontSize = defaultAppearance?.FontSize,
                            TextColor = ColorToHex(defaultAppearance?.TextColor),
                            BorderColor = ColorToHex(field.Characteristics?.Border),
                            BackgroundColor = ColorToHex(field.Characteristics?.Background),
                            // BorderInfo does not expose a Width property in Aspose.Pdf, so we leave it null.
                            BorderWidth = null
                        };

                        appearances.Add(appearance);
                    }

                    // Serialize the list to JSON with indentation for readability
                    var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(appearances, jsonOptions);
                    File.WriteAllText(outputJsonPath, json);
                    Console.WriteLine($"Form field appearance settings exported to '{outputJsonPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }

        // Helper to convert System.Drawing.Color (or nullable) to a hex string (e.g., "#RRGGBB")
        private static string? ColorToHex(System.Drawing.Color? color)
        {
            if (color == null) return null;
            return $"#{color.Value.R:X2}{color.Value.G:X2}{color.Value.B:X2}";
        }

        // Overload for Aspose.Pdf.Color – convert via ToRgb() before extracting components.
        private static string? ColorToHex(Aspose.Pdf.Color? color)
        {
            if (color == null) return null;
            // Aspose.Pdf.Color does not expose R/G/B directly; use ToRgb() to get System.Drawing.Color.
            System.Drawing.Color rgb = color.ToRgb();
            return $"#{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}";
        }
    }
}
