using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Names of the fields (adjust to your PDF)
            string sourceFieldName = "SourceField"; // field whose value drives the visibility
            string targetFieldName = "TargetField"; // field to be shown/hidden

            // Verify that both fields exist
            if (!form.HasField(sourceFieldName))
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' not found.");
                return;
            }
            if (!form.HasField(targetFieldName))
            {
                Console.Error.WriteLine($"Target field '{targetFieldName}' not found.");
                return;
            }

            // Retrieve the fields via the indexer. The indexer returns a WidgetAnnotation, so cast to Field.
            Field sourceField = form[sourceFieldName] as Field;
            Field targetField = form[targetFieldName] as Field;

            if (sourceField == null || targetField == null)
            {
                Console.Error.WriteLine("One of the fields could not be cast to a Form Field.");
                return;
            }

            // JavaScript that toggles the visibility of the target field.
            // The script is attached to the source field's *OnCalculate* action – this
            // action is executed whenever the field's value changes.
            string js = $@"
var tgt = this.getField('{targetFieldName}');
if (event.value == 'Show')
    tgt.display = display.visible;
else
    tgt.display = display.hidden;
";

            // Create a JavascriptAction with the script
            JavascriptAction jsAction = new JavascriptAction(js);

            // Assign the JavaScript to the source field's OnCalculate action
            sourceField.Actions.OnCalculate = jsAction;

            // OPTIONAL: Ensure the target field is initially hidden.
            // HideAction hides the field when the page is opened (OnEnter).
            HideAction initialHide = new HideAction(targetFieldName, true);
            targetField.Actions.OnEnter = initialHide;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with conditional visibility to '{outputPath}'.");
    }
}
