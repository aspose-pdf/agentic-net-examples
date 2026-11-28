using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for DefaultAppearance
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for Font and FontName

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "fonts.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(pdfPath);

        // Prepare CSV rows
        var rows = new List<string>();
        rows.Add("FieldName,FontName"); // CSV header

        // Iterate over all form fields
        foreach (Field field in doc.Form)
        {
            // Field name – use FullName if available, otherwise PartialName
            string fieldName = field.FullName ?? field.PartialName ?? string.Empty;

            // Default font name (when not applicable)
            string fontName = "N/A";

            // Try to obtain the font for known field types via DefaultAppearance
            switch (field)
            {
                case TextBoxField txtField when txtField.DefaultAppearance?.Font != null:
                    fontName = txtField.DefaultAppearance.Font.FontName;
                    break;
                case ComboBoxField comboField when comboField.DefaultAppearance?.Font != null:
                    fontName = comboField.DefaultAppearance.Font.FontName;
                    break;
                case ListBoxField listField when listField.DefaultAppearance?.Font != null:
                    fontName = listField.DefaultAppearance.Font.FontName;
                    break;
                case RadioButtonField radioField when radioField.DefaultAppearance?.Font != null:
                    fontName = radioField.DefaultAppearance.Font.FontName;
                    break;
                case CheckboxField checkField when checkField.DefaultAppearance?.Font != null:
                    fontName = checkField.DefaultAppearance.Font.FontName;
                    break;
                // Add other field types if needed
            }

            // Escape commas for CSV compliance
            string escapedField = fieldName.Contains(",") ? $"\"{fieldName}\"" : fieldName;
            string escapedFont = fontName.Contains(",") ? $"\"{fontName}\"" : fontName;

            rows.Add($"{escapedField},{escapedFont}");
        }

        // Write the collected data to CSV
        try
        {
            File.WriteAllLines(csvPath, rows);
            Console.WriteLine($"Font information exported to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing CSV: {ex.Message}");
        }
    }
}
