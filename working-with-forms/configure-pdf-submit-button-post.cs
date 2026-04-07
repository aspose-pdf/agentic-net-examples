using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with a form
        const string outputPdf = "output.pdf";  // PDF with configured submit button

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        // Create a FormEditor facade bound to the loaded document
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // Define button placement (lower‑left and upper‑right corners)
            float llx = 100f; // lower‑left X
            float lly = 200f; // lower‑left Y
            float urx = 200f; // upper‑right X
            float ury = 250f; // upper‑right Y

            // Add a submit button named "btnSubmit" on page 1
            formEditor.AddSubmitBtn(
                fieldName: "btnSubmit",
                page: 1,
                label: "Submit",
                url: "https://example.com/submit",
                llx: llx,
                lly: lly,
                urx: urx,
                ury: ury);

            // Configure the button to submit data using HTTP POST
            // ExportFormat (HTML) ensures URL‑encoded form data.
            // Do NOT set GetMethod flag; absence means POST.
            formEditor.SetSubmitFlag("btnSubmit", SubmitFormFlag.Html);

            // (Optional) If you need to change the URL after adding the button:
            // formEditor.SetSubmitUrl("btnSubmit", "https://example.com/submit");

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Submit button configured and saved to '{outputPdf}'.");
    }
}