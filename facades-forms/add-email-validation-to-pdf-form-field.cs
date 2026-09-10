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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // NOTE: The PropertyFlag.Validate enum member was removed in recent Aspose.PDF versions.
                // Validation is now performed via JavaScript attached to the field, so the SetFieldAttribute call
                // is no longer required and has been removed.

                // Add JavaScript that checks the field value against an email regex pattern
                string js = @"
if (!/^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$/.test(event.value)) {
    app.alert('Invalid email address.');
    event.rc = false; // reject the input
}";
                formEditor.SetFieldScript("Email", js);

                // Save the modified PDF
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Email field validation applied and saved to '{outputPdf}'.");
    }
}
