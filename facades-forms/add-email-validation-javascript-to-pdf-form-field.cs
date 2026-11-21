using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF using the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // JavaScript that validates the email format on blur event
            // It checks the field value against a simple regex and shows an alert if invalid.
            string emailJs = @"
if (event.value != null && !event.value.match(/^\S+@\S+\.\S+$/)) {
    app.alert('Invalid email address');
    event.rc = false; // prevent the field from losing focus
}
";

            // Attach the JavaScript to the field named "Email"
            // FormEditor uses AddFieldScript for this purpose.
            formEditor.AddFieldScript("Email", emailJs);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript added and saved to '{outputPath}'.");
    }
}
