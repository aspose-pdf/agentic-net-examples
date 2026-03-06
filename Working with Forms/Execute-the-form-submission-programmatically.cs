using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (must contain a form) and the output PDF
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_submit.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF using the FormEditor facade and add a submit button
        // -----------------------------------------------------------------
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPdf);

            // Add a submit button named "SubmitBtn" on page 1.
            // Parameters: fieldName, pageNumber, label, url, llx, lly, urx, ury
            formEditor.AddSubmitBtn(
                fieldName: "SubmitBtn",
                page: 1,
                label: "Submit",
                url: "https://example.com/submit",
                llx: 100f,
                lly: 500f,
                urx: 200f,
                ury: 540f);

            // Set the submit flag so that the whole PDF is submitted (PDF format)
            formEditor.SetSubmitFlag("SubmitBtn", SubmitFormFlag.Pdf);

            // Save the modified PDF with the new submit button
            formEditor.Save(outputPdf);
        }

        // ---------------------------------------------------------------
        // 2. Verify the submit button's flags using the Form facade (optional)
        // ---------------------------------------------------------------
        using (Form form = new Form(outputPdf))
        {
            // Retrieve the flags associated with the submit button
            SubmitFormFlag flags = form.GetSubmitFlags("SubmitBtn");
            Console.WriteLine($"Submit button flags: {flags}");
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}