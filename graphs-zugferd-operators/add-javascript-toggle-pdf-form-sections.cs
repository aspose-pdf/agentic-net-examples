using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF with form fields
        const string outputPath = "output_with_js.pdf"; // PDF after adding JavaScript

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Assume there is a choice field (e.g., a combo box) named
            // "OptionField". When its value changes we want to show or hide
            // optional sections "SectionA" and "SectionB".
            // ------------------------------------------------------------

            // Retrieve the field by its full name. The indexer returns a
            // WidgetAnnotation, so we cast it to the base Field type.
            Field optionField = doc.Form["OptionField"] as Field;
            if (optionField == null)
            {
                Console.Error.WriteLine("Field 'OptionField' not found in the document.");
                return;
            }

            // Build the JavaScript code.
            // The script checks the selected value and sets the display
            // property of the target fields accordingly.
            string js = @"
var opt = event.value;
var secA = this.getField('SectionA');
var secB = this.getField('SectionB');

if (opt == 'ShowA')
{
    secA.display = display.visible;
    secB.display = display.hidden;
}
else if (opt == 'ShowB')
{
    secA.display = display.hidden;
    secB.display = display.visible;
}
else
{
    secA.display = display.hidden;
    secB.display = display.hidden;
}
";

            // Attach the JavaScript action to a supported event. For form
            // fields the OnCalculate action is triggered when the field's
            // value changes, which is the appropriate place for our script.
            optionField.Actions.OnCalculate = new JavascriptAction(js);

            // ------------------------------------------------------------
            // Save the modified PDF. The Document.Save(string) method always
            // writes a PDF regardless of the file extension, which is the
            // desired behavior here.
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
