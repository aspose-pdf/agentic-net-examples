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

        using (Document doc = new Document(inputPath))
        {
            const string sourceFieldName = "SourceField";
            const string targetFieldName = "TargetField";

            // Retrieve the source field as a Form field (cast from WidgetAnnotation)
            Field sourceField = doc.Form[sourceFieldName] as Field;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Source field '{sourceFieldName}' not found.");
                return;
            }

            // JavaScript that hides/shows the target field based on the source checkbox value.
            string js = $@"
                var src = this.getField('{sourceFieldName}');
                var tgt = this.getField('{targetFieldName}');
                if (src.value == 'Yes')
                {{
                    tgt.display = display.hidden;
                }}
                else
                {{
                    tgt.display = display.visible;
                }}
            ";

            // Use a supported action – OnCalculate is invoked when the field value changes.
            sourceField.Actions.OnCalculate = new JavascriptAction(js);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
