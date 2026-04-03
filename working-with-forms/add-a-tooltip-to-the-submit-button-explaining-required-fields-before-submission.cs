using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor facade to modify the PDF form
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPath);

            // Add a submit button named "SubmitBtn" on page 1
            // Parameters: field name, page number, button label, submit URL,
            // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            editor.AddSubmitBtn(
                "SubmitBtn",
                1,
                "Submit",
                "https://example.com/submit",
                100f,   // llx
                100f,   // lly
                200f,   // urx
                130f    // ury
            );

            // Retrieve the created button field to set its tooltip.
            // The tooltip is stored in the AlternateName property.
            ButtonField submitBtn = editor.Document.Form["SubmitBtn"] as ButtonField;
            if (submitBtn != null)
            {
                submitBtn.AlternateName = "Please fill all required fields before submitting.";
            }

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip: {outputPath}");
    }
}