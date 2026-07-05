using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Assume there is a dropdown (ChoiceField) named "SelectionField"
            // and a text box (TextBoxField) named "OptionalSection" that should
            // be shown or hidden based on the dropdown value.
            // ------------------------------------------------------------

            // Retrieve the choice field (dropdown)
            ChoiceField dropdown = doc.Form["SelectionField"] as ChoiceField;
            if (dropdown == null)
            {
                Console.Error.WriteLine("Dropdown field 'SelectionField' not found.");
                return;
            }

            // Retrieve the optional section field (e.g., a text box)
            TextBoxField optionalSection = doc.Form["OptionalSection"] as TextBoxField;
            if (optionalSection == null)
            {
                Console.Error.WriteLine("Optional section field 'OptionalSection' not found.");
                return;
            }

            // Initially hide the optional section using a document‑level JavaScript action.
            JavascriptAction hideOnOpen = new JavascriptAction(
                "var f = this.getField('OptionalSection'); f.display = display.hidden;");
            doc.OpenAction = hideOnOpen; // DocumentActionCollection does not expose OnOpen; use OpenAction.

            // JavaScript to toggle visibility when the dropdown value changes.
            // When the selected value equals "Show", make the optional section visible;
            // otherwise hide it.
            string jsCode = @"
var f = this.getField('OptionalSection');
if (event.value == 'Show')
{
    f.display = display.visible;
}
else
{
    f.display = display.hidden;
}";

            // Attach the script to the dropdown's value‑change event.
            // For form fields the valid property is OnValidate (or OnCalculate).
            dropdown.Actions.OnValidate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
