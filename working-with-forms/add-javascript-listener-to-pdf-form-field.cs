using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // required for Field and form actions
using Aspose.Pdf.Annotations;   // required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "MyField"; // name of the field to monitor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Attach a JavaScript action that runs when the field value is changed.
            // The OnValidate action is invoked after the user modifies the field.
            field.Actions.OnValidate = new JavascriptAction(
                "app.alert('The value of the field has changed.');"
            );

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with JavaScript listener to '{outputPath}'.");
    }
}