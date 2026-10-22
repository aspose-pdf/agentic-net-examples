using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction
using Aspose.Pdf.Forms;      // ButtonField

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

        // JavaScript that resets the form and clears hidden fields
        string js = @"
this.resetForm();
for (var i = 0; i < this.numFields; i++) {
    var f = this.getField(this.getFieldName(i));
    if (f.display == display.hidden) {
        f.value = '';
    }
}";

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Attach JavaScript to the button field named "ResetForm"
            if (doc.Form != null && doc.Form["ResetForm"] != null)
            {
                var resetButton = (ButtonField)doc.Form["ResetForm"];
                // Use the button's mouse‑press action to execute the script (OnPressMouseBtn is a valid property)
                resetButton.Actions.OnPressMouseBtn = new JavascriptAction(js);
            }
            else
            {
                Console.Error.WriteLine("Button field 'ResetForm' not found.");
            }

            // Add a document‑level script that runs when the PDF is opened
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript attached to 'ResetForm' at '{outputPath}'.");
    }
}
