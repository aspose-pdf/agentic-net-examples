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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume the checkbox field is named "ShowDetails"
            // and the field to hide/show is named "DetailsField"
            const string checkboxName = "ShowDetails";
            const string targetFieldName = "DetailsField";

            // Retrieve the checkbox field (correct class name is CheckboxField)
            CheckboxField checkBox = doc.Form[checkboxName] as CheckboxField;
            if (checkBox == null)
            {
                Console.Error.WriteLine($"Checkbox field '{checkboxName}' not found.");
                doc.Save(outputPath); // Save unchanged PDF
                return;
            }

            // JavaScript to hide the target field when the checkbox is unchecked,
            // and show it when checked.
            // In PDF JavaScript, the field's value is "Yes" when checked by default.
            string js = $@"
var f = this.getField('{targetFieldName}');
if (event.target.value == 'Yes') {{
    f.display = display.visible;
}} else {{
    f.display = display.hidden;
}}";

            // Assign the script to the checkbox's mouse‑release action (the
            // appropriate event for a checkbox in Aspose.PDF)
            checkBox.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // Optionally, set the initial visibility based on the current state
            bool isChecked = checkBox.Checked; // use the Checked property, not Value
            var targetField = doc.Form[targetFieldName];
            if (targetField != null)
            {
                // Hide the field when unchecked, otherwise ensure it is visible
                targetField.Flags = isChecked ? AnnotationFlags.Default : AnnotationFlags.Hidden;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
