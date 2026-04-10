using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document API (no Facades)
        Document pdfDoc = new Document(inputPath);

        // Example modification: set a form field value
        // Replace "FieldName" with the actual field name in the PDF
        const string fieldName = "FieldName";

        // Retrieve the field directly via the indexer. It returns null if the field does not exist.
        var field = pdfDoc.Form != null ? pdfDoc.Form[fieldName] : null;

        if (field != null)
        {
            // Handle the most common field types explicitly; fallback to reflection for generic fields.
            if (field is TextBoxField txt)
            {
                txt.Value = "New Value";
            }
            else if (field.GetType().Name == "CheckBoxField")
            {
                // Use dynamic to access the Checked property that may not be present in the compile‑time type.
                dynamic chk = field;
                chk.Checked = true; // example for a checkbox
            }
            else if (field.GetType().Name == "RadioButtonOptionField")
            {
                dynamic rad = field;
                rad.Checked = true; // example for a radio button
            }
            else
            {
                // Generic handling – try to set a "Value" property via reflection if it exists.
                var prop = field.GetType().GetProperty("Value");
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(field, "New Value");
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' does not support setting a value directly.");
                }
            }
        }
        else
        {
            Console.Error.WriteLine($"Form field '{fieldName}' not found.");
        }

        // Save the modified PDF to the output path
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Form‑modified PDF saved to '{outputPath}'.");
    }
}
