using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document first – FormEditor expects a Document instance, not a file path.
        using (Document pdfDocument = new Document(inputPdf))
        using (FormEditor formEditor = new FormEditor(pdfDocument))
        {
            // JavaScript that validates the field value against a simple e‑mail regex.
            // If the value does not match, an alert is shown and the validation fails (event.rc = false).
            string emailValidationScript = @"
if (!/^[\w\.\-]+@([\w\-]+\.)+[\w\-]{2,4}$/.test(event.value)) {
    app.alert('Invalid email address.');
    event.rc = false;
}";
            // Attach the script to the "Email" field. This script runs on the field's Validate event.
            formEditor.SetFieldScript("Email", emailValidationScript);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Email field validation added. Saved to '{outputPdf}'.");
    }
}
