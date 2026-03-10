using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the input PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name (replace "MyField" with the actual field name)
            // Document.Form indexer returns a WidgetAnnotation; cast it to Field to work with form‑field members.
            Field? field = doc.Form["MyField"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Form field 'MyField' not found or is not a form field.");
                return;
            }

            // Attach JavaScript to the field's focus (OnEnter) and blur (OnExit) events.
            // In Aspose.Pdf the corresponding actions are named OnEnter and OnExit.
            field.Actions.OnEnter = new JavascriptAction("app.alert('Field gained focus');");
            field.Actions.OnExit  = new JavascriptAction("app.alert('Field lost focus');");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript actions to '{outputPath}'.");
    }
}
