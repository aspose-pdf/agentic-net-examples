using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing a submit button named "SubmitForm"
        const string outputPdf = "output.pdf";     // PDF with JavaScript attached to the submit button

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor facade to modify the form.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // JavaScript that shows a confirmation dialog.
            // If the user selects "No", the submission is cancelled (event.rc = false).
            string jsCode = "if (app.alert('Are you sure you want to submit?', 3) != 4) { event.rc = false; }";

            // Attach the script to the submit button named "SubmitForm".
            editor.AddFieldScript("SubmitForm", jsCode);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}