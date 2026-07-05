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
        const string outputPath = "output_with_validation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a submit button on the first page
            // Rectangle(left, bottom, width, height)
            ButtonField submitBtn = new ButtonField(doc, new Aspose.Pdf.Rectangle(100, 100, 200, 130))
            {
                Name        = "SubmitBtn",
                PartialName = "SubmitBtn",
                Contents    = "Submit"
            };

            // JavaScript that validates required fields before submitting
            // Adjust the field names in the array as needed for your form
            string jsCode = @"
var requiredFields = ['Name', 'Email']; // add all required field names here
for (var i = 0; i < requiredFields.length; i++) {
    var f = this.getField(requiredFields[i]);
    if (f && f.required && (f.value == '' || f.value == null)) {
        app.alert('Please fill the required field: ' + requiredFields[i]);
        // Prevent the button action from proceeding
        event.rc = false;
        break;
    }
}
if (event.rc != false) {
    // Submit the form if all required fields are filled
    this.submitForm({cURL: 'https://example.com/submit'});
}
";

            // Attach the JavaScript action to the button using a valid action property
            // OnPressMouseBtn fires when the button is pressed (mouse down)
            submitBtn.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the document's form
            doc.Form.Add(submitBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with validation script: '{outputPath}'.");
    }
}
