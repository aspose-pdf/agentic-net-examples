using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form fields (TextBoxField, CheckboxField, etc.)
using Aspose.Pdf.Annotations;    // JavascriptAction

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPath  = "input.pdf";          // Existing PDF with form fields (optional)
        const string outputPath = "output.pdf";

        // If the source PDF does not exist, create a new one with the required fields
        if (!File.Exists(inputPath))
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a single page
                Page page = doc.Pages.Add();

                // ----- Target field (the field to hide/show) -----
                // Rectangle: lower‑left X/Y, upper‑right X/Y (1‑based page coordinates)
                Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
                TextBoxField targetField = new TextBoxField(page, targetRect)
                {
                    PartialName = "targetField",
                    Value       = "Sample text"
                };
                doc.Form.Add(targetField);

                // ----- Checkbox field (controller) -----
                Aspose.Pdf.Rectangle checkRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);
                CheckboxField checkBox = new CheckboxField(page, checkRect)
                {
                    PartialName = "myCheckBox",
                    // Initial state: unchecked (value = "Off")
                    Checked = false
                };
                doc.Form.Add(checkBox);

                // JavaScript that hides the target field when the checkbox is unchecked
                // and shows it when checked. The field name must match the PartialName above.
                string js = @"if (event.target.value == 'Off')
{
    this.getField('targetField').display = display.hidden;
}
else
{
    this.getField('targetField').display = display.visible;
}";
                // Assign the script to the mouse‑press event (valid action property)
                checkBox.Actions.OnPressMouseBtn = new JavascriptAction(js);

                // Save the newly created PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Created PDF with interactive fields: '{outputPath}'");
            return;
        }

        // ----- Existing PDF path -----
        // Load the existing PDF, locate the checkbox, and attach the JavaScript action.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the checkbox by its partial name. Adjust the name if different.
            CheckboxField checkBox = doc.Form["myCheckBox"] as CheckboxField;

            if (checkBox == null)
            {
                Console.Error.WriteLine("Checkbox field 'myCheckBox' not found in the document.");
                return;
            }

            // JavaScript identical to the one used during creation.
            string js = @"if (event.target.value == 'Off')
{
    this.getField('targetField').display = display.hidden;
}
else
{
    this.getField('targetField').display = display.visible;
}";

            // Attach the script to the mouse‑press event.
            checkBox.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to: '{outputPath}'");
    }
}
