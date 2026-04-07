using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "MyField"; // name of the field to monitor

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by its name and cast to Field (WidgetAnnotation derives from Field)
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Create a JavaScript action that will be executed when the field value changes.
            // The OnValidate action is invoked whenever the user modifies the field's contents.
            JavascriptAction jsAction = new JavascriptAction("myFunction();");
            field.Actions.OnValidate = jsAction;

            // Define the JavaScript function at the document level.
            // Document.OpenAction is used because Document.JavaScript.Add does not exist.
            doc.OpenAction = new JavascriptAction(
                "function myFunction() { app.alert('Field value changed!'); }");

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript listener to '{outputPdf}'.");
    }
}