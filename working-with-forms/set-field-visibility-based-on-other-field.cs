using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Ensure the source and target fields exist
            if (!form.HasField("SourceField") || !form.HasField("TargetField"))
            {
                Console.Error.WriteLine("Required fields 'SourceField' or 'TargetField' are missing.");
                return;
            }

            // Retrieve the source field (cast to Field to access the Actions collection)
            Field sourceField = (Field)form["SourceField"];

            // JavaScript that toggles visibility of TargetField based on SourceField's value
            string js = @"
                var src = this.getField('SourceField').value;
                var tgt = this.getField('TargetField');
                if (src == 'Hide')
                {
                    tgt.display = display.hidden;
                }
                else
                {
                    tgt.display = display.visible;
                }
            ";

            // Attach the JavaScript to the source field's OnCalculate event (executed when the field value changes)
            sourceField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
