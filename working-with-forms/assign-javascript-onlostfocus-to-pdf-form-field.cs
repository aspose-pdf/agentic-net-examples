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

        // Load the PDF document (standard load rule)
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field to access form‑specific members
            Field? field = doc.Form["myField"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Form field 'myField' not found or is not a form field.");
                return;
            }

            // JavaScript that runs when the field loses focus – doubles the numeric value
            string jsCode = @"
                var val = parseFloat(event.target.value);
                if (!isNaN(val)) {
                    event.target.value = (val * 2).toString();
                }
            ";

            field.Actions.OnLostFocus = new JavascriptAction(jsCode);

            // Save the modified PDF (standard save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action: {outputPath}");
    }
}
