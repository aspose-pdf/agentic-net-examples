using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with form fields
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure automatic recalculation is enabled
            doc.Form.AutoRecalculate = true;

            // Names of the numeric fields to be summed
            string[] numericFieldNames = { "Field1", "Field2", "Field3" };
            const string totalFieldName = "Total";

            // Verify that the total field exists and is a text box
            if (!doc.Form.HasField(totalFieldName))
            {
                Console.Error.WriteLine($"Total field \"{totalFieldName}\" not found.");
                return;
            }

            // Cast the total field to TextBoxField to access its Actions
            TextBoxField totalField = doc.Form[totalFieldName] as TextBoxField;
            if (totalField == null)
            {
                Console.Error.WriteLine($"Field \"{totalFieldName}\" is not a text box.");
                return;
            }

            // Build the JavaScript that sums the numeric fields
            // In PDF JavaScript, 'event.value' is the value of the field whose
            // calculate action is being executed.
            // 'this.getField(name).value' retrieves the current value of another field.
            var jsBuilder = new System.Text.StringBuilder();
            jsBuilder.Append("event.value = ");
            for (int i = 0; i < numericFieldNames.Length; i++)
            {
                jsBuilder.Append($"this.getField('{numericFieldNames[i]}').value");
                if (i < numericFieldNames.Length - 1)
                    jsBuilder.Append(" + ");
            }
            jsBuilder.Append(";");

            // Assign the JavaScript to the total field's calculate action
            totalField.Actions.OnCalculate = new JavascriptAction(jsBuilder.ToString());

            // Optionally, add a document‑level script that runs when the document opens
            // (e.g., to initialize fields). This is not required for the calculation itself.
            // doc.OpenAction = new JavascriptAction("app.alert('Form ready');");

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPdf}'.");
    }
}