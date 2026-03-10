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
        const string submitUrl = "https://example.com/submit";

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
                // Add a submit button named "SubmitBtn" on page 1
                // with label "Submit" and set its action URL to the desired endpoint
                formEditor.AddSubmitBtn(
                    fieldName: "SubmitBtn",
                    page: 1,
                    label: "Submit",
                    url: submitUrl,
                    llx: 100,   // lower‑left X
                    lly: 700,   // lower‑left Y
                    urx: 200,   // upper‑right X
                    ury: 750    // upper‑right Y
                );

                // Save the modified PDF with the submit action
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with submit action saved to '{outputPdf}'.");
    }
}