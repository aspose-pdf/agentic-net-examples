using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // existing PDF with a form
        const string outputPdf = "output_with_confirm.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a submit button named "SubmitBtn" on page 1
                // Parameters: field name, page number, button caption, submit URL,
                // llx, lly, urx, ury (coordinates in points)
                formEditor.AddSubmitBtn(
                    "SubmitBtn",          // field name
                    1,                    // page number (1‑based)
                    "Submit",             // button caption
                    "https://example.com/submit", // target URL
                    100, 500, 200, 550); // rectangle bounds

                // Attach JavaScript to the button that shows a confirmation dialog.
                // If the user clicks "Yes" (returns 0), the form is submitted.
                string js = @"
if (app.alert('Do you want to submit the form?', 2) == 0) {
    this.submitForm();
}";
                formEditor.AddFieldScript("SubmitBtn", js);
            }

            // Save the modified PDF (the Document.Save method is the required lifecycle operation)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with confirmation script: {outputPdf}");
    }
}