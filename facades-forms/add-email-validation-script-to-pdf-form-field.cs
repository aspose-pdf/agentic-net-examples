using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the "Email" field
        const string outputPdf = "output.pdf";  // PDF with validation script added

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with input and output files
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // JavaScript that validates the field value against a simple e‑mail regex.
            // It runs when the field loses focus (on blur) and shows an alert if the value is invalid.
            string emailValidationScript = @"
                var email = this.getField('Email').value;
                var pattern = /^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$/;
                if (!pattern.test(email)) {
                    app.alert('Please enter a valid e‑mail address.');
                    this.getField('Email').setFocus();
                }
            ";

            // Attach the script to the "Email" field.
            // The script will be executed on the field's "Validate" event.
            formEditor.AddFieldScript("Email", emailValidationScript);

            // Save the modified PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Email field validation script added. Output saved to '{outputPdf}'.");
    }
}