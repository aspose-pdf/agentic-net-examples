using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class FillFormExample
{
    static void Main()
    {
        const string inputPath = "form_input.pdf";
        const string outputPath = "filled_output.pdf";
        const string jsonPath = "form_fields.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the AcroForm object
            Form acroForm = doc.Form;

            // Disable automatic recalculation for better performance when many fields are set
            acroForm.AutoRecalculate = false;

            // NOTE: The DefaultAppearance class may not be available in older Aspose.Pdf versions.
            // If needed, uncomment the line below and ensure the correct version is referenced.
            // acroForm.DefaultAppearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);

            // Fill a text field named "CustomerName" if it exists
            if (acroForm.HasField("CustomerName"))
            {
                // The indexer returns a Field; cast to the appropriate derived type
                var textField = acroForm["CustomerName"] as TextBoxField;
                if (textField != null)
                {
                    textField.Value = "John Doe";
                }
            }

            // Set a checkbox field named "Subscribe" if it exists
            if (acroForm.HasField("Subscribe"))
            {
                var field = acroForm["Subscribe"];
                if (field != null && field.GetType().Name == "CheckBoxField")
                {
                    // Use dynamic to access the Checked property without a compile‑time dependency
                    dynamic checkBox = field;
                    checkBox.Checked = true;
                }
            }

            // Add an additional appearance of a field on page 1 at a specific rectangle
            if (acroForm.HasField("Signature"))
            {
                var signatureField = acroForm["Signature"] as SignatureField;
                if (signatureField != null)
                {
                    Aspose.Pdf.Rectangle appearanceRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
                    // AddFieldAppearance expects a Field instance, not a WidgetAnnotation
                    acroForm.AddFieldAppearance(signatureField, 1, appearanceRect);
                }
            }

            // Export current field values to JSON (default options)
            acroForm.ExportToJson(jsonPath, new ExportFieldsToJsonOptions());

            // Example of re‑importing the JSON data (uncomment if needed)
            // acroForm.ImportFromJson(jsonPath);

            // Flatten the form so fields become part of the page content
            acroForm.Flatten();

            // Save the modified PDF (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form processing completed. Output saved to '{outputPath}'.");
    }
}
