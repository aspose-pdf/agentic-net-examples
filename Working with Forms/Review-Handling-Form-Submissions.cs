using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for SubmitFormFlag enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Add a submit button to the existing PDF using FormEditor.
        // -----------------------------------------------------------------
        // FormEditor can be instantiated without a document and later bound
        // to the PDF via BindPdf(string). This follows the recommended
        // lifecycle pattern for Facades classes.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPdf);

            // Add a submit button named "btnSubmit" on page 1.
            // Parameters: field name, page number, label, URL, llx, lly, urx, ury
            editor.AddSubmitBtn(
                fieldName: "btnSubmit",
                page: 1,
                label: "Submit",
                url: "https://example.com/submit",
                llx: 100f,
                lly: 200f,
                urx: 200f,
                ury: 250f);

            // Set the submission flag – for example, submit the whole PDF.
            editor.SetSubmitFlag("btnSubmit", SubmitFormFlag.Pdf);

            // Save the modified document.
            editor.Save(outputPdf);
        }

        // -----------------------------------------------------------------
        // 2. Read back the submit button's flags using the Form facade.
        // -----------------------------------------------------------------
        using (Form form = new Form(outputPdf))
        {
            // Retrieve the flags for the button we just added.
            SubmitFormFlag flags = form.GetSubmitFlags("btnSubmit");

            // Display which flags are set.
            Console.WriteLine("Submit button flags:");
            Console.WriteLine($"  PDF  : {(flags & SubmitFormFlag.Pdf) != 0}");
            Console.WriteLine($"  HTML : {(flags & SubmitFormFlag.Html) != 0}");
            Console.WriteLine($"  XFDF : {(flags & SubmitFormFlag.Xfdf) != 0}");
            Console.WriteLine($"  FDF  : {(flags & SubmitFormFlag.Fdf) != 0}");
        }

        Console.WriteLine($"Form submission button added and saved to '{outputPdf}'.");
    }
}