using System;
using System.IO;
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

        // Open the PDF for form editing
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // JavaScript that validates the Email field using a regular expression
            string validationScript = @"
var email = event.value;
var re = /^[\w\-\.+]+@([\w\-]+\.)+[\w\-]{2,4}$/;
if (!re.test(email)) {
    app.alert('Invalid email address');
    event.rc = false;
}";
            // Attach the script to the field named "Email"
            formEditor.SetFieldScript("Email", validationScript);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with email validation: '{outputPath}'.");
    }
}