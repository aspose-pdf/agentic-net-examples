using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Edit the PDF form using FormEditor (facade)
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF
            formEditor.BindPdf(inputPdf);

            // Name of the submit button to which the script will be attached.
            // Adjust this name to match the actual button in your PDF.
            const string submitButtonName = "btnSubmit";

            // JavaScript that shows a confirmation dialog.
            // If the user clicks "OK" (value 4), the form is submitted.
            string jsCode = "if (app.alert('Are you sure you want to submit?', 3, 2) == 4) { this.submitForm(); }";

            // Attach the script to the push‑button field.
            formEditor.AddFieldScript(submitButtonName, jsCode);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPdf}'.");
    }
}