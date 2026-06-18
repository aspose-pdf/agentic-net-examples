using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Initialize FormEditor with source and destination files
        using (FormEditor formEditor = new FormEditor(sourcePdf, outputPdf))
        {
            // Define button position (lower‑left and upper‑right corners)
            float llx = 50f;   // lower‑left X
            float lly = 750f;  // lower‑left Y
            float urx = 150f;  // upper‑right X
            float ury = 800f;  // upper‑right Y

            // Add a submit button named "SubmitForm" on page 1
            formEditor.AddSubmitBtn(
                fieldName: "SubmitForm",
                page: 1,
                label: "Submit",
                url: "https://api.example.com/submit",
                llx: llx,
                lly: lly,
                urx: urx,
                ury: ury);

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Submit button added and saved to '{outputPdf}'.");
    }
}