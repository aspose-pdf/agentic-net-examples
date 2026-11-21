using System;
using System.IO;
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

        // FormEditor is the correct class for editing form structure
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // JavaScript validation for the "Email" field using a regular expression
            // The regex is escaped for the C# string and then embedded in the JS literal
            string emailRegex = "^[\\w\\.-]+@[\\w\\.-]+\\.\\w+$";
            string script = $"if (!/{emailRegex}/.test(event.value)) {{ app.alert('Invalid email address'); event.rc = false; }}";

            // Apply the validation script to the field named "Email"
            editor.SetFieldScript("Email", script);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Email field regex validation applied and saved to '{outputPath}'.");
    }
}
