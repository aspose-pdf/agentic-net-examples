using System;
using System.IO;
using Aspose.Pdf.Facades; // Fully qualified namespace for FormEditor and related classes

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Existing PDF form
        const string outputPath = "output.pdf";  // Resulting PDF with JavaScript
        const string buttonName = "btnSubmit";   // Name of the submit button
        const string submitUrl  = "https://www.example.com/submit";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // JavaScript that checks required fields before submitting.
        // Adjust the field names in the array to match those in your PDF.
        string validationScript = @"
var required = ['FirstName','LastName','Email'];
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (!f || f.value == '') {
        app.alert('Please fill the required field: ' + required[i]);
        return false;
    }
}
this.submitForm();
";

        // Use FormEditor (which implements IDisposable) inside a using block.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Add a submit button on page 1. Coordinates are in points.
            // (llx, lly) = lower‑left corner, (urx, ury) = upper‑right corner.
            formEditor.AddSubmitBtn(buttonName, 1, "Submit", submitUrl, 10f, 200f, 70f, 270f);

            // Attach the validation JavaScript to the button.
            formEditor.SetFieldScript(buttonName, validationScript);

            // Persist changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"PDF with JavaScript‑enabled submit button saved to '{outputPath}'.");
    }
}