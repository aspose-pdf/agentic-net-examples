using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "PhoneNumber";
        const string mask      = "(###) ###-####";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Add JavaScript to enforce the input mask.
        // The script runs on the field's Keystroke event and formats the value.
        // This is a simple example; a more robust script may be required for production use.
        string js = @"
            var re = /\D/g;
            var clean = event.value.replace(re, '');
            if (clean.length > 0) {
                var formatted = '';
                if (clean.length >= 1) formatted += '(' + clean.substring(0, Math.min(3, clean.length));
                if (clean.length >= 4) formatted += ') ' + clean.substring(3, Math.min(6, clean.length));
                if (clean.length >= 7) formatted += '-' + clean.substring(6, Math.min(10, clean.length));
                event.value = formatted;
            }
        ";
        // Attach the script to the field. The second parameter is the JavaScript code.
        formEditor.SetFieldScript(fieldName, js);

        // Optionally, set the field as required or read‑only using SetFieldAttribute.
        // Here we keep the field editable, so no flag is set.

        // Save the modified PDF.
        formEditor.Save();

        // Clean up resources.
        formEditor.Close();

        Console.WriteLine($"PhoneNumber field updated with input mask and saved to '{outputPdf}'.");
    }
}