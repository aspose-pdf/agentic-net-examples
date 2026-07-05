using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_submit.pdf"; // destination PDF

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a FormEditor facade with source and destination paths
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a submit button named "SubmitForm" on page 1.
            // Parameters: fieldName, page (1‑based), label, URL, llx, lly, urx, ury
            formEditor.AddSubmitBtn(
                fieldName: "SubmitForm",
                page: 1,
                label: "SubmitForm",
                url: "https://api.example.com/submit",
                llx: 100f,
                lly: 100f,
                urx: 200f,
                ury: 150f);

            // Persist the changes to the destination file
            formEditor.Save();
        }

        Console.WriteLine($"Submit button added and saved to '{outputPdf}'.");
    }
}