using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
            // Assume the form already contains a checkbox named "ShowOptional"
            // and two optional fields named "OptionalSection1" and "OptionalSection2".
            // Retrieve the checkbox field from the form.
            // ------------------------------------------------------------
            CheckboxField toggleField = doc.Form["ShowOptional"] as CheckboxField;
            if (toggleField == null)
            {
                Console.Error.WriteLine("Checkbox field 'ShowOptional' not found.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // JavaScript that toggles visibility of the optional sections
            string jsCode = @"
var show = this.getField('ShowOptional').value;
if (show == 'Yes' || show == true) {
    this.getField('OptionalSection1').display = display.visible;
    this.getField('OptionalSection2').display = display.visible;
} else {
    this.getField('OptionalSection1').display = display.hidden;
    this.getField('OptionalSection2').display = display.hidden;
}
";

            // Attach the JavaScript action to the checkbox's calculation event.
            // AnnotationActionCollection does not have an Add method; instead, assign
            // the appropriate action property (OnCalculate works for value‑change).
            toggleField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
