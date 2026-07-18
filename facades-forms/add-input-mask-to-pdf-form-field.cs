using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_masked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and bind it to the FormEditor facade
        using (Document doc = new Document(inputPath))
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(doc);

            // JavaScript that validates the phone number format "(###) ###-####"
            string js = @"
var re = /^\(\d{3}\) \d{3}\-\d{4}$/;
if (!re.test(event.value)) {
    app.alert('Please enter the phone number in the format (###) ###-####');
    event.rc = false;
}";
            // Attach the script to the field named "PhoneNumber"
            formEditor.AddFieldScript("PhoneNumber", js);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with input mask to '{outputPath}'.");
    }
}