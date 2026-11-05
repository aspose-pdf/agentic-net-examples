using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for Border and BorderStyle

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_validated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Name of the required field in the PDF form
            const string fieldName = "Name";

            // Verify the field exists – use HasField, not Contains
            if (!doc.Form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // Cast the field to a TextBoxField (adjust type if different)
            TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                return;
            }

            // Determine if the field is empty after submission
            string value = txtField.Value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(value))
            {
                // Required field is empty – display a red border
                txtField.Characteristics.Border = System.Drawing.Color.Red; // fully qualified to avoid ambiguity
                txtField.Border = new Border(txtField) { Width = 2, Style = BorderStyle.Solid };
            }
            else
            {
                // Field has a value – reset to normal appearance
                txtField.Characteristics.Border = System.Drawing.Color.Black; // fully qualified to avoid ambiguity
                txtField.Border = new Border(txtField) { Width = 1, Style = BorderStyle.Solid };
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Validated PDF saved to '{outputPath}'.");
    }
}
