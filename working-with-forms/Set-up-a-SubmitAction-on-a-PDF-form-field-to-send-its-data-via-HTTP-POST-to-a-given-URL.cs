using System;
using System.IO;
using Aspose.Pdf.Facades;          // FormEditor, SubmitFormFlag
using Aspose.Pdf;                  // For Color if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";                 // Source PDF with a form
        const string outputPdf = "output.pdf";                // Destination PDF
        const string submitUrl = "https://example.com/submit"; // URL to receive POST data

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF using FormEditor (facade for form operations)
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Add a submit button named "btnSubmit" on page 1.
            // Rectangle coordinates are in points: llx, lly, urx, ury.
            formEditor.AddSubmitBtn(
                fieldName: "btnSubmit",
                page: 1,
                label: "Submit",
                url: submitUrl,
                llx: 100f,
                lly: 700f,
                urx: 200f,
                ury: 750f);

            // Set the submission format. Html flag causes the data to be sent as an HTML form via HTTP POST.
            formEditor.SetSubmitFlag("btnSubmit", SubmitFormFlag.Html);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with SubmitAction saved to '{outputPdf}'.");
    }
}