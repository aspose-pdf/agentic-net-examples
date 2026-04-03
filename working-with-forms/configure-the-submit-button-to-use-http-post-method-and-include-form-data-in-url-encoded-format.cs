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
        const string buttonName = "submitBtn";
        const string buttonLabel = "Submit";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a submit button on page 1 (coordinates: llx, lly, urx, ury)
                formEditor.AddSubmitBtn(buttonName, 1, buttonLabel, submitUrl, 100, 500, 200, 550);

                // Set the submit flag to HTML (URL‑encoded) which uses HTTP POST by default
                formEditor.SetSubmitFlag(buttonName, SubmitFormFlag.Html);

                // Ensure the button points to the correct URL
                formEditor.SetSubmitUrl(buttonName, submitUrl);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Submit button configured and saved to '{outputPdf}'.");
    }
}