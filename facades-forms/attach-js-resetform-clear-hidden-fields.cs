using System;
using System.IO;
using Aspose.Pdf;
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

        // JavaScript that resets the form and clears all hidden fields
        string js = @"
this.resetForm();
var n = this.numFields;
for (var i = 0; i < n; i++) {
    var f = this.getField(this.getFieldName(i));
    if (f && f.type == 'hidden') {
        f.value = '';
    }
}
";

        // Attach the script to the button named "ResetForm" using FormEditor (Facades API)
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);                     // Load the PDF
            formEditor.AddFieldScript("ResetForm", js);       // Set button‑level JavaScript
            formEditor.Save(outputPath);                       // Save the modified PDF
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
