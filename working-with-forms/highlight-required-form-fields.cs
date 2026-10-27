using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Names of fields that are required
        string[] requiredFields = { "FirstName", "LastName", "Email" };

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            if (doc.Form != null && doc.Form.Count > 0)
            {
                foreach (string fieldName in requiredFields)
                {
                    // Retrieve the field by its name; returns null if not present
                    var field = doc.Form[fieldName];
                    if (field != null)
                    {
                        // Only TextBoxField (and similar) support a background‑like color.
                        // In the current Aspose.PDF version the form field classes expose a
                        // "Color" property (border color) but not a "BackgroundColor" property.
                        // Setting the Color to a light‑yellow gives a visual highlight that
                        // works across versions.
                        if (field is TextBoxField textBox)
                        {
                            // Highlight the field with a light‑yellow color
                            textBox.Color = Color.FromRgb(1.0, 1.0, 0.8);
                        }
                        else if (field is ComboBoxField comboBox)
                        {
                            comboBox.Color = Color.FromRgb(1.0, 1.0, 0.8);
                        }
                        // Add other field types that expose a Color property if needed.
                    }
                }
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}
