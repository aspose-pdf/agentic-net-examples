using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that validates the "Age" field. It shows an alert if the value is less than 18.
        string js = @"
            var f = this.getField('Age');
            if (f.value != '' && parseInt(f.value) < 18) {
                app.alert('Age must be at least 18 years old.');
            }
        ";

        // Attach the script using FormEditor (Aspose.Pdf.Facades).
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPdf);
        editor.SetFieldScript("Age", js);
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}
