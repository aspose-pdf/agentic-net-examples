using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Fill a text field named "Name" if it exists
            Field nameField = doc.Form["Name"] as Field; // cast WidgetAnnotation to Field
            if (nameField != null)
            {
                // Prefer the concrete TextBoxField when possible
                if (nameField is TextBoxField txtField)
                {
                    txtField.Value = "John Doe";
                }
                else
                {
                    nameField.Value = "John Doe";
                }
            }

            // Set a checkbox field named "Agree" if it exists
            Field agreeField = doc.Form["Agree"] as Field; // cast WidgetAnnotation to Field
            if (agreeField != null)
            {
                if (agreeField is CheckboxField cbField)
                {
                    cbField.Checked = true;
                }
                else
                {
                    agreeField.Value = "Yes";
                }
            }

            // Flatten the form fields so they become non‑editable
            // This removes all form fields and places their values directly on the page
            doc.Form.Flatten();

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
