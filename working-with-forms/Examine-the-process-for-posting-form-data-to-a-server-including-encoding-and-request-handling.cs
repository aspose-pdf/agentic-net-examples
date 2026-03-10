using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a submit button named "btnSubmit" on page 1
                // Rectangle coordinates: lower‑left (10,200), upper‑right (70,270)
                formEditor.AddSubmitBtn(
                    fieldName: "btnSubmit",
                    page: 1,
                    label: "Submit",
                    url: submitUrl,
                    llx: 10f,
                    lly: 200f,
                    urx: 70f,
                    ury: 270f);

                // Set the submission flags.
                // Html flag causes the form data to be sent in HTML format (GET request).
                formEditor.SetSubmitFlag("btnSubmit", SubmitFormFlag.Html);

                // Optionally, change the URL after creation
                formEditor.SetSubmitUrl("btnSubmit", submitUrl);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // Demonstrate reading the submit button flags using the Form facade
        // -----------------------------------------------------------------
        using (Form form = new Form(outputPdf))
        {
            // Retrieve the flags for the submit button we just created
            SubmitFormFlag flags = form.GetSubmitFlags("btnSubmit");

            Console.WriteLine($"Submit button flags: {flags}");

            // Example: check if the Html flag is set
            bool isHtml = (flags & SubmitFormFlag.Html) != 0;
            Console.WriteLine($"Is HTML submission enabled? {isHtml}");
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}