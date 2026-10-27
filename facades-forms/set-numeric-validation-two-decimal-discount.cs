using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // JavaScript that validates the field value:
        // - Allows only digits with an optional decimal point
        // - Limits to two digits after the decimal
        string js = @"
if (event.value) {
    var re = /^\d+(\.\d{1,2})?$/;
    if (!re.test(event.value)) {
        app.alert('Please enter a numeric value with up to two decimal places.');
        event.rc = false;
    }
}";

        // Retrieve the "Discount" field and attach the validation script
        var field = pdfDocument.Form["Discount"] as TextBoxField;
        if (field != null)
        {
            // Use the correct Aspose class name (lower‑case 's') from the Annotations namespace
            field.Actions.OnValidate = new JavascriptAction(js);
        }
        else
        {
            Console.Error.WriteLine("Field 'Discount' not found.");
        }

        // Save the updated PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Saved PDF with numeric validation on 'Discount' field to '{outputPath}'.");
    }
}