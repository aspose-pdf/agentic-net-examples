using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF with form fields (or create a new one)
        const string outputPath = "form_with_validation.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // ------------------------------------------------------------
            // Example: add a required text field (if not already present)
            // ------------------------------------------------------------
            // Rectangle coordinates are (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField txtField = new TextBoxField(doc, txtRect)
            {
                PartialName = "CustomerName",
                Required    = true,                     // mark as required
                Contents    = ""                        // initial empty value
            };
            form.Add(txtField);

            // ------------------------------------------------------------
            // Add a submit button with JavaScript validation
            // ------------------------------------------------------------
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);
            ButtonField submitBtn = new ButtonField(doc, btnRect)
            {
                PartialName = "SubmitBtn",
                // Optional visual properties
                Color   = Aspose.Pdf.Color.LightGray,
                Contents = "Submit"
            };

            // JavaScript that checks all required fields before submitting
            string jsCode = @"
var fieldNames = this.getFieldNames();
for (var i = 0; i < fieldNames.length; i++) {
    var f = this.getField(fieldNames[i]);
    if (f.required && (f.value === '' || f.value === null)) {
        app.alert('Please fill all required fields before submitting.');
        // Prevent submission
        return;
    }
}
// All required fields are filled – perform the submit
this.submitForm({cURL:'https://example.com/receive'});
";

            // Attach the JavaScript action to the button (Mouse Up / Release event)
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            submitBtn.Actions.OnReleaseMouseBtn = jsAction; // single JavaScript action performs validation and submit

            // Add the button to the form
            form.Add(submitBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validation saved to '{outputPath}'.");
    }
}
