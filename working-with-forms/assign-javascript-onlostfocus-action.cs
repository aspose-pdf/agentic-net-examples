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
        const string fieldName = "myField"; // name of the field to attach the script

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by name and cast to Aspose.Pdf.Forms.Field
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
                return;
            }

            // JavaScript to calculate a value when the field loses focus.
            // Example: sum of two other fields named "field1" and "field2"
            string js = @"
                var a = this.getField('field1').value;
                var b = this.getField('field2').value;
                this.value = (parseFloat(a) + parseFloat(b)).toString();
            ";

            // Assign the JavaScript action to the OnLostFocus event
            field.Actions.OnLostFocus = new JavascriptAction(js);

            // Ensure the form recalculates automatically
            doc.Form.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action: '{outputPath}'.");
    }
}