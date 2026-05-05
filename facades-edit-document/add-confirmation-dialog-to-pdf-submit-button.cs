using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitUrl  = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit the form using FormEditor (Aspose.Pdf.Facades)
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF
            formEditor.BindPdf(inputPath);

            // Add a submit button named "SubmitBtn" on page 1
            // Parameters: field name, page number, button caption, submit URL,
            //             lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            formEditor.AddSubmitBtn(
                "SubmitBtn",
                1,
                "Submit",
                submitUrl,
                100, 100, 200, 150);

            // JavaScript that shows a confirmation dialog.
            // app.alert returns 4 for "Yes" when using the 3‑button style.
            string js = "if(app.alert('Are you sure you want to submit?', 3) == 4) this.submitForm();";

            // Attach the script to the submit button.
            formEditor.AddFieldScript("SubmitBtn", js);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with confirmation dialog saved to '{outputPath}'.");
    }
}