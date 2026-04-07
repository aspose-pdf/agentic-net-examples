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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the source field is a checkbox named "ShowDetails"
            // and the target field to hide/show is a text box named "DetailsBox".
            const string sourceFieldName = "ShowDetails";
            const string targetFieldName = "DetailsBox";

            // Verify that both fields exist
            if (!doc.Form.HasField(sourceFieldName))
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' not found.");
                return;
            }
            if (!doc.Form.HasField(targetFieldName))
            {
                Console.Error.WriteLine($"Target field '{targetFieldName}' not found.");
                return;
            }

            // JavaScript that toggles the visibility of the target field
            // based on the checkbox value. In Acrobat JavaScript, "display.hidden"
            // hides the field, "display.visible" shows it.
            string js = $@"
if (event.value == 'Yes')
    this.getField('{targetFieldName}').display = display.hidden;
else
    this.getField('{targetFieldName}').display = display.visible;
";
            JavascriptAction jsAction = new JavascriptAction(js);

            // Retrieve the source field via the indexer. The indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before using form‑specific members.
            Field sourceField = doc.Form[sourceFieldName] as Field;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' is not a form field.");
                return;
            }
            // Attach the JavaScript to the field's OnCalculate event – this event fires when the field's value changes.
            sourceField.Actions.OnCalculate = jsAction;

            // Ensure the target field is visible when the document is opened.
            // HideAction with "hide = false" makes the field visible on page open.
            Field targetField = doc.Form[targetFieldName] as Field;
            if (targetField == null)
            {
                Console.Error.WriteLine($"Target field '{targetFieldName}' is not a form field.");
                return;
            }
            targetField.Actions.OnOpenPage = new HideAction(targetFieldName, false);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
