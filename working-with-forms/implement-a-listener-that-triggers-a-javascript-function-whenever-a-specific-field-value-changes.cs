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
        const string outputPath = "output_with_listener.pdf";
        const string fieldName  = "MyTextField"; // name of the field to monitor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the field by its full name (or partial name)
            Field targetField = null;
            foreach (Field f in doc.Form.Fields)
            {
                if (f.FullName.Equals(fieldName, StringComparison.OrdinalIgnoreCase) ||
                    f.PartialName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    targetField = f;
                    break;
                }
            }

            if (targetField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Attach a JavaScript action that runs when the field value changes.
            // The OnValidate action is triggered after the user modifies the field.
            string jsCode = "app.alert('Field value changed!');";
            targetField.Actions.OnValidate = new JavascriptAction(jsCode);

            // Optionally, you can also execute the script immediately for testing:
            // targetField.ExecuteFieldJavaScript(new JavascriptAction(jsCode));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript listener: {outputPath}");
    }
}