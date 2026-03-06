using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, Form, SubmitFormFlag

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Create a FormEditor instance, bind the existing PDF,
        // add a submit button, set its submission flag, and save.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Add a submit button named "mySubmit" on page 1.
            // Parameters: field name, page number (1‑based), button label,
            // target URL, lower‑left X/Y, upper‑right X/Y.
            editor.AddSubmitBtn(
                fieldName: "mySubmit",
                page: 1,
                label: "Submit",
                url: "https://example.com/submit",
                llx: 100f,
                lly: 500f,
                urx: 200f,
                ury: 550f);

            // Set the button to submit the whole PDF (SubmitFormFlag.Pdf).
            editor.SetSubmitFlag("mySubmit", SubmitFormFlag.Pdf);

            // Persist the changes to a new file.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Form saved successfully to '{outputPdf}'.");
    }
}