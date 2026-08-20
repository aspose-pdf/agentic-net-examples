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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Names of the fields
            const string sourceFieldName = "sourceField"; // field whose value controls visibility
            const string targetFieldName = "targetField"; // field to be shown/hidden

            // Retrieve the source and target fields (they are returned as WidgetAnnotation, so cast to Field)
            Field sourceField = (Field)doc.Form[sourceFieldName];
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' not found.");
                return;
            }

            Field targetField = (Field)doc.Form[targetFieldName];
            if (targetField == null)
            {
                Console.Error.WriteLine($"Target field '{targetFieldName}' not found.");
                return;
            }

            // JavaScript that toggles the visibility of the target field based on the source field's value.
            string js = $@"
if (event.value == 'Yes')
{{
    this.getField('{targetFieldName}').display = display.visible;
}}
else
{{
    this.getField('{targetFieldName}').display = display.hidden;
}}
";

            // Attach the JavaScript to the source field's OnCalculate action (the valid trigger for value‑change scripts).
            sourceField.Actions.OnCalculate = new JavascriptAction(js);

            // Optionally set the initial visibility of the target field when the document is opened.
            // This can be done with a separate JavaScript on the document level or by evaluating the current value now.
            bool initiallyVisible = string.Equals(sourceField.Value?.ToString(), "Yes", StringComparison.OrdinalIgnoreCase);
            // Use a HideAction on the target field to set its initial state.
            targetField.Actions.OnCalculate = new HideAction(targetFieldName, !initiallyVisible);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
