using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string resetButtonName = "ResetBtn"; // name of the push‑button field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF into the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // JavaScript that resets the whole form and then clears hidden fields.
            // Replace "Hidden1" and "Hidden2" with the actual hidden field names.
            string js = @"
                // Reset all form fields
                this.resetForm();

                // List of hidden fields to clear
                var hiddenFields = ['Hidden1', 'Hidden2'];

                // Iterate and clear each hidden field
                for (var i = 0; i < hiddenFields.length; i++) {
                    var f = this.getField(hiddenFields[i]);
                    if (f) { f.value = ''; }
                }
            ";

            // Attach the script to the specified push‑button field
            formEditor.SetFieldScript(resetButtonName, js);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and PDF saved to '{outputPdf}'.");
    }
}