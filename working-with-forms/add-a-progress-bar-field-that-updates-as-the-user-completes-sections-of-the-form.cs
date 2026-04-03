using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for Border and JavascriptAction
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // existing PDF with form fields
        const string outputPath = "output_with_progress.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the progress bar (simulated with a text box) will appear
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 300, 770);

            // Create a TextBoxField that will act as the progress indicator
            TextBoxField progressField = new TextBoxField(doc, progressRect)
            {
                PartialName = "ProgressBar",
                Value = "0%",
                Color = Color.LightGray,                     // background colour
                TextHorizontalAlignment = HorizontalAlignment.Center,
                TextVerticalAlignment = VerticalAlignment.Center,
                ReadOnly = true
            };

            // Border must be created after the field instance because it requires the parent annotation
            progressField.Border = new Border(progressField) { Width = 1 };
            // Border colour is taken from the field's own Color property; the background is already set above.

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based indexing)
            doc.Form.Add(progressField, 1);

            // OPTIONAL: Add a simple JavaScript action that could be attached to other fields
            // to update the progress value. Here we just demonstrate how to create the action.
            string jsCode = @"
                var totalSections = 5;               // total number of sections
                var completed = 0;
                // Count filled fields (simple example)
                for (var i = 0; i < this.numFields; i++) {
                    var f = this.getField(this.getFieldName(i));
                    if (f.value && f.value != '') completed++;
                }
                var percent = Math.round((completed / totalSections) * 100);
                this.getField('ProgressBar').value = percent + '%';
            ";
            // NOTE: The correct class name is JavascriptAction (lower‑case 's')
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Example: attach to the first existing field (if any)
            if (doc.Form.Count > 0)
            {
                // The first field in the form collection – cast to Field because the indexer returns a WidgetAnnotation
                Field firstField = doc.Form[1] as Field;
                if (firstField != null)
                {
                    firstField.Actions.OnExit = jsAction; // trigger when leaving the field
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }
}
