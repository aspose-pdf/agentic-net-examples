using System;
using System.IO;
using System.Drawing;                     // for Color handling
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;               // for Border and BorderStyle

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string csvPath = "borders.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Ensure the document actually contains a form with fields
        if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Length == 0)
        {
            Console.Error.WriteLine("No form fields found in the PDF.");
            return;
        }

        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            // CSV header
            writer.WriteLine("FieldName,BorderStyle,BorderWidth,BorderColor");

            foreach (Field field in doc.Form.Fields)
            {
                // ----- Border Style -----
                string style = string.Empty;
                string width = string.Empty;
                if (field.Border != null)
                {
                    // Border.Style is an enum (BorderStyle)
                    style = field.Border.Style.ToString();
                    // Width is an int (or float depending on version)
                    width = field.Border.Width.ToString();
                }

                // ----- Border Color -----
                string color = string.Empty;
                // Characteristics holds the colour information (System.Drawing.Color)
                if (field.Characteristics != null)
                {
                    // Convert the System.Drawing.Color to a hex string for CSV readability
                    color = ColorTranslator.ToHtml(field.Characteristics.Border);
                }

                // Escape field name for CSV
                string name = field.Name?.Replace("\"", "\"\"") ?? string.Empty;
                if (name.Contains(",") || name.Contains("\""))
                    name = $"\"{name}\"";

                writer.WriteLine($"{name},{style},{width},{color}");
            }
        }

        Console.WriteLine($"Exported border styles to '{csvPath}'.");
    }
}
