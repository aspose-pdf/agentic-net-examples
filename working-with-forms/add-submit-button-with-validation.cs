using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Define the submit button name and its position on page 1
            string submitButtonName = "SubmitBtn";
            int pageNumber = 1;
            float llx = 100f; // lower‑left x
            float lly = 50f;  // lower‑left y
            float urx = 200f; // upper‑right x
            float ury = 80f;  // upper‑right y

            // Add a submit button (if it already exists this will add another one)
            formEditor.AddSubmitBtn(submitButtonName, pageNumber, "Submit", "https://example.com/submit", llx, lly, urx, ury);

            // JavaScript that checks required fields before allowing submission
            // Adjust the field names in the array to match the actual required fields in your PDF
            string validationJs = @"
var requiredFields = ['FirstName','LastName','Email'];
for (var i = 0; i < requiredFields.length; i++) {
    var f = this.getField(requiredFields[i]);
    if (!f || f.value === '') {
        // Hide/disable the submit button and alert the user
        this.getField('" + submitButtonName + @"').display = display.hidden;
        app.alert('Please fill all required fields before submitting.');
        break;
    }
}
";

            // Attach the JavaScript to the submit button (executed on mouse up)
            formEditor.AddFieldScript(submitButtonName, validationJs);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with client‑side validation saved to '{outputPdf}'.");
    }
}