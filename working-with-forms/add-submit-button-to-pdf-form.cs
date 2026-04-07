using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Contains SubmitFormFlag enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with the source PDF and the destination path.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a submit button named "SubmitBtn" on page 1.
            // The button rectangle is defined by lower‑left (100,100) and upper‑right (200,130).
            formEditor.AddSubmitBtn(
                fieldName: "SubmitBtn",
                page: 1,
                label: "Submit",
                url: submitUrl,
                llx: 100,
                lly: 100,
                urx: 200,
                ury: 130);

            // Configure the button to submit the whole PDF file.
            formEditor.SetSubmitFlag("SubmitBtn", SubmitFormFlag.Pdf);

            // Save the modified document.
            formEditor.Save();
        }

        Console.WriteLine($"Submit button added and saved to '{outputPdf}'.");
    }
}