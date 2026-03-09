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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Edit the PDF form using FormEditor (facade)
        using (FormEditor editor = new FormEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdf);

            // Add a submit button named "btnSubmit" on page 1
            // Parameters: field name, page number (1‑based), label, URL, llx, lly, urx, ury
            editor.AddSubmitBtn(
                fieldName: "btnSubmit",
                page: 1,
                label: "Submit",
                url: "https://example.com/submit",
                llx: 100f,
                lly: 500f,
                urx: 200f,
                ury: 550f);

            // Set the desired submit flags (e.g., submit as PDF and XFDF)
            editor.SetSubmitFlag("btnSubmit", SubmitFormFlag.Pdf | SubmitFormFlag.Xfdf);

            // Optionally update the URL after creation
            editor.SetSubmitUrl("btnSubmit", "https://example.com/submit");

            // Save the modified document
            editor.Save(outputPdf);
        }

        // Verify the flags using the Form facade
        using (Form form = new Form(outputPdf))
        {
            SubmitFormFlag flags = form.GetSubmitFlags("btnSubmit");
            Console.WriteLine($"Submit button flags: {flags}");
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}