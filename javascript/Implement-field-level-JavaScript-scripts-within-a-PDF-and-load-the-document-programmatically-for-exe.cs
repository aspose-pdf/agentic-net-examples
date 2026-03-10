using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing a form field
        const string outputPdf = "output.pdf";  // PDF after JavaScript execution
        const string fieldName = "MyTextField"; // name of the field to run script on

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field by its name. The Form collection returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field before using field‑specific members.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Create a JavaScript action. This example shows an alert dialog.
            JavascriptAction jsAction = new JavascriptAction("app.alert('JavaScript executed from field.');");

            // Execute the JavaScript action for the specified field
            field.ExecuteFieldJavaScript(jsAction);

            // Save the modified document (still PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript executed and document saved to '{outputPdf}'.");
    }
}
