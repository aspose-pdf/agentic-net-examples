using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";
        const string checkBoxName = "myCheckBox"; // replace with actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the checkbox field by name using the indexer on Form
                var field = doc.Form[checkBoxName];
                if (field == null)
                {
                    Console.Error.WriteLine($"Checkbox field '{checkBoxName}' not found.");
                    return;
                }

                if (!(field is CheckboxField checkBox))
                {
                    Console.Error.WriteLine($"Field '{checkBoxName}' is not a checkbox.");
                    return;
                }

                // ---- Modify appearance ----
                // Change the rectangle (position/size) of the checkbox.
                // Use Aspose.Pdf.Rectangle (the type expected by form fields).
                checkBox.Rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

                // Note: Appearance‑related properties such as CheckBoxAppearance, BorderColor,
                // and BackgroundColor are not available in the current Aspose.Pdf version,
                // so they are omitted. If needed, they can be set via the underlying widget
                // dictionary or by upgrading to a newer library version.

                // ---- Extract the checkbox value ----
                bool isChecked = checkBox.Checked;
                Console.WriteLine($"Checkbox '{checkBoxName}' is {(isChecked ? "checked" : "unchecked")}.");

                // Save the modified PDF (optional)
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
