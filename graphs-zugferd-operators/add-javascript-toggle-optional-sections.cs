using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form pdfForm = doc.Form;

            // ------------------------------------------------------------
            // Example: A checkbox named "ShowOptional" controls visibility
            // of a group of fields whose names start with "Optional_".
            // ------------------------------------------------------------

            // Retrieve the controlling checkbox field (safely cast)
            if (pdfForm["ShowOptional"] is CheckboxField toggleField)
            {
                // JavaScript that toggles visibility of all fields whose name
                // starts with "Optional_". In PDF JavaScript, the 'event' object
                // provides the value of the field that triggered the action.
                string js = @"
var show = (event.target.value == 'Yes'); // adjust value as needed
var fields = ['Optional_Section1', 'Optional_Section2', 'Optional_Section3'];
for (var i = 0; i < fields.length; i++) {
    var f = this.getField(fields[i]);
    if (f != null) {
        f.display = show ? display.visible : display.hidden;
    }
}
";

                // Attach the JavaScript to the checkbox's mouse‑press action.
                // This runs when the user clicks the checkbox.
                toggleField.Actions.OnPressMouseBtn = new JavascriptAction(js);
            }

            // ------------------------------------------------------------
            // If you have a dropdown (ChoiceField) that should control
            // visibility, you can use a similar approach:
            // ------------------------------------------------------------
            // if (pdfForm["SelectionCombo"] is ChoiceField combo)
            // {
            //     string comboJs = @"
            // var selected = event.target.value;
            // var f1 = this.getField('Optional_Section1');
            // var f2 = this.getField('Optional_Section2');
            // if (selected == 'OptionA') {
            //     f1.display = display.visible;
            //     f2.display = display.hidden;
            // } else if (selected == 'OptionB') {
            //     f1.display = display.hidden;
            //     f2.display = display.visible;
            // } else {
            //     f1.display = display.hidden;
            //     f2.display = display.hidden;
            // }
            // ";
            //     combo.Actions.OnPressMouseBtn = new JavascriptAction(comboJs);
            // }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
