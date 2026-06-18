using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF for form editing; output will be written to outputPath
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // JavaScript that validates the field value against a simple e‑mail regex.
            // If the value does not match, an alert is shown and the field change is rejected.
            string emailValidationScript = @"
if (!/^[\w\.-]+@[\w\.-]+\.[A-Za-z]{2,}$/.test(event.value)) {
    app.alert('Invalid email address');
    event.rc = false;
}";

            // Attach the validation script to the existing field named "Email"
            formEditor.SetFieldScript("Email", emailValidationScript);

            // Persist the changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Email field validation added and saved to '{outputPath}'.");
    }
}