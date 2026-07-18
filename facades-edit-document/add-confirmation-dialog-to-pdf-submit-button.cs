using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Edit the PDF form using FormEditor (Facade API)
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Add a submit button named "ConfirmSubmit" on page 1
            // Rectangle coordinates: lower‑left (100,500) upper‑right (200,550)
            // Empty URL and SubmitPdf flag (default) are used; the actual submission
            // will be triggered by the JavaScript attached to the button.
            editor.AddSubmitBtn("ConfirmSubmit", 1, "", "SubmitPdf", 100, 500, 200, 550);

            // JavaScript that shows a confirmation dialog.
            // app.alert returns 1 for "OK" when the third argument (type) is 3 (Yes/No).
            string js = "if(app.alert('Are you sure you want to submit?', 3) == 1) this.submitForm();";

            // Attach the script to the button.
            editor.AddFieldScript("ConfirmSubmit", js);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with confirmation dialog saved to '{outputPath}'.");
    }
}