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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a submit button on page 1
                // Rectangle coordinates: lower‑left (100,100), upper‑right (200,130)
                formEditor.AddSubmitBtn("submitBtn", 1, "Submit", "https://example.com/submit", 100, 100, 200, 130);

                // JavaScript that checks all required fields before allowing submission
                string js = @"
var fields = this.getFields();
for (var i = 0; i < fields.length; i++) {
    if (fields[i].required && (fields[i].value == null || fields[i].value == '')) {
        app.alert('Please fill all required fields before submitting.');
        // Cancel the submit action
        event.rc = false;
        break;
    }
}
";

                // Attach the JavaScript to the submit button
                formEditor.AddFieldScript("submitBtn", js);

                // Save the modified PDF with validation logic
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with required‑field validation saved to '{outputPdf}'.");
    }
}