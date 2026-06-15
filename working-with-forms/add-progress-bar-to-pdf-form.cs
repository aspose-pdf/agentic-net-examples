using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // existing PDF with form fields
        const string outputPath = "output_with_progress.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a progress bar field (simple TextBoxField showing percentage)
            // Rectangle coordinates: left, bottom, right, top
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);
            TextBoxField progressField = new TextBoxField(doc, progressRect)
            {
                Name = "ProgressBar",          // internal name
                PartialName = "ProgressBar",   // displayed name (tooltip)
                Value = "0%"                    // initial value
            };

            // Add the field to the form
            doc.Form.Add(progressField);

            // Define a simple JavaScript function that updates the progress bar.
            // The function expects a numeric percentage (0‑100) and sets the field value.
            string jsFunction = @"function updateProgress(percent) {
    var f = this.getField('ProgressBar');
    if (f != null) {
        f.value = percent + '%';
    }
}";

            // Add the script to the document‑level JavaScript collection using the indexer.
            doc.JavaScript["UpdateProgress"] = jsFunction;

            // Example: attach a JavaScript action to an existing field (if any) to call the function.
            // Here we demonstrate with the first field on the first page.
            if (doc.Form.Count > 0)
            {
                // Get the first field (Aspose.Pdf collections are 1‑based). The collection returns a WidgetAnnotation.
                var widget = doc.Form[1];
                // Cast to Field to access the Actions collection.
                Field firstField = widget as Field;
                if (firstField != null)
                {
                    // Add an OnExit action that calls updateProgress with a dummy value.
                    // In a real scenario, you would calculate the actual completion percentage.
                    firstField.Actions.OnExit = new JavascriptAction("updateProgress(10);");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }
}
