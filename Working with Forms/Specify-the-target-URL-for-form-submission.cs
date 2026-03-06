using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF with a form (or empty PDF)
        const string outputPdf = "output_with_submit.pdf";
        const string submitUrl = "https://www.example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor is a Facade class used to manipulate AcroForm fields.
        // It does not require a Document instance in the constructor; we bind the PDF later.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document into the editor.
            formEditor.BindPdf(inputPdf);

            // Add a submit button named "submitBtn" on page 1.
            // Parameters: field name, page number (1‑based), button label, target URL,
            // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            formEditor.AddSubmitBtn(
                fieldName: "submitBtn",
                page: 1,
                label: "Submit",
                url: submitUrl,
                llx: 100f,
                lly: 200f,
                urx: 150f,
                ury: 250f);

            // Alternatively, if a submit button already exists, you could set its URL:
            // formEditor.SetSubmitUrl("existingButtonName", submitUrl);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}