using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_listener.pdf";
        const string targetFieldName = "myField"; // name of the field to monitor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by its name; the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field.
            Field field = doc.Form[targetFieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{targetFieldName}' not found or is not a form field.");
                return;
            }

            // Attach a JavaScript action that runs when the field value changes (OnValidate event)
            // The JavaScript simply shows an alert; replace with any desired function.
            field.Actions.OnValidate = new JavascriptAction("app.alert('The value of the field has changed.');");

            // Optionally, you can also execute the script immediately for testing:
            // field.ExecuteFieldJavaScript(new JavascriptAction("app.alert('Test execution');"));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript listener to '{outputPath}'.");
    }
}
