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

        // Bind the PDF to the FormEditor facade
        Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor();
        formEditor.BindPdf(inputPath);

        // Set maximum characters for the phone number field (including formatting characters)
        formEditor.SetFieldLimit("PhoneNumber", 14);

        // JavaScript to enforce the mask "(###) ###‑####"
        string js = @"
var f = this.getField('PhoneNumber');
if (event.willCommit) {
    var val = event.value.replace(/\D/g, '');
    if (val.length > 0) {
        var formatted = '(' + val.substring(0,3);
        if (val.length > 3) formatted += ') ' + val.substring(3,6);
        if (val.length > 6) formatted += '-' + val.substring(6,10);
        event.value = formatted;
    }
}";
        formEditor.SetFieldScript("PhoneNumber", js);

        // Save the updated PDF
        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine($"PhoneNumber field updated with mask and saved to '{outputPath}'.");
    }
}