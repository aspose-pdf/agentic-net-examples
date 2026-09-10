using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF, attach JavaScript to the "Age" field, and save.
        using (FormEditor formEditor = new FormEditor())
        {
            // Initialize the facade with the source PDF.
            formEditor.BindPdf(inputPdf);

            // JavaScript that shows a warning if the entered value is less than 18.
            string js = @"
                if (event.value < 18) {
                    app.alert('Age must be at least 18 years old.');
                }
            ";

            // Attach the script to the field named "Age".
            // SetFieldScript works for any field; the script will be executed on field validation.
            formEditor.SetFieldScript("Age", js);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}