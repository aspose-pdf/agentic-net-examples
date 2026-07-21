using System;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_renamed.pdf";

        // ------------------------------------------------------------
        // Create a minimal PDF with a form field so the example can run
        // in the sandbox where no external files exist.
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a page
            Page page = seed.Pages.Add();

            // Define a rectangle for the field (left, bottom, right, top)
            var fieldRect = new Aspose.Pdf.Rectangle(100, 700, 250, 720);

            // Create a text box form field with a non‑camelCase name
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "First_Name", // example name to be converted to camelCase
                Value = "John Doe"
            };
            // Add the field to the document's form collection
            seed.Form.Add(txtField);

            // Save the seed PDF that will be used as input
            seed.Save(inputPath);
        }

        // Load the PDF document that contains the form fields
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all form field names via the Form.Fields collection
            var fieldNames = doc.Form.Fields
                .OfType<Field>()
                .Select(f => f.PartialName)
                .ToArray();

            // Initialize the FormEditor for batch renaming
            using (FormEditor editor = new FormEditor(doc))
            {
                foreach (string oldName in fieldNames)
                {
                    string newName = ToCamelCase(oldName);
                    if (!string.Equals(oldName, newName, StringComparison.Ordinal))
                    {
                        // Rename each field to its camelCase version
                        editor.RenameField(oldName, newName);
                    }
                }

                // Save the updated PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }

    // Simple camelCase conversion: lower the first character
    static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLowerInvariant(input[0]) + input.Substring(1);
    }
}
