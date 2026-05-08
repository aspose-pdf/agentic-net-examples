using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";
        const string resetButtonName = "ResetForm";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        // Initialize the FormEditor facade on the loaded document
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // JavaScript that clears hidden fields and then resets the form
            string js = @"
                var fieldNames = this.getFieldNames();
                for (var i = 0; i < fieldNames.length; i++) {
                    var f = this.getField(fieldNames[i]);
                    if (f.display == display.hidden) {
                        f.value = '';
                    }
                }
                this.resetForm();
            ";

            // Attach the script to the reset button field
            formEditor.AddFieldScript(resetButtonName, js);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}