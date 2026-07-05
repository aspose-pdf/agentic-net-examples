using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing the form
        const string outputPath = "output.pdf"; // Resulting PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Names of the fields (adjust to your PDF)
            const string sourceFieldName = "CheckBox1";   // Field whose value controls visibility
            const string targetFieldName = "TextField1";  // Field to hide/show

            // Retrieve the source field (the field that triggers the action)
            Field sourceField = form[sourceFieldName] as Field;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' not found.");
                return;
            }

            // JavaScript that toggles the visibility of the target field
            // display.visible = 0, display.hidden = 1, display.noPrint = 2
            string js = $@"
if (event.value == 'Yes')
{{
    this.getField('{targetFieldName}').display = display.visible;
}}
else
{{
    this.getField('{targetFieldName}').display = display.hidden;
}}";

            // Attach the JavaScript to a supported action – OnPressMouseBtn works for check‑boxes
            sourceField.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with conditional visibility to '{outputPath}'.");
    }
}
