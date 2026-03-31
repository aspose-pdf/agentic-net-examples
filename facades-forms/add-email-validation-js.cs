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

        // JavaScript that validates the email format on blur (Validate event)
        string js = @"if (event.target.value != null) {
    var email = event.target.value;
    var re = /^[\w\.-]+@[\w\.-]+\.\w+$/;
    if (!re.test(email)) {
        app.alert('Invalid email address');
        event.rc = false;
    }
}";

        // Initialize FormEditor with input and output PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        bool scriptAdded = formEditor.AddFieldScript("Email", js);
        if (!scriptAdded)
        {
            Console.Error.WriteLine("Failed to add JavaScript to the 'Email' field.");
        }
        formEditor.Save();
        Console.WriteLine($"JavaScript added and saved to '{outputPath}'.");
    }
}