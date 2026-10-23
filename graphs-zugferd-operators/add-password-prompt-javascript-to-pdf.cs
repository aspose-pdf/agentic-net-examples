using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that runs when the document is opened.
            // It prompts the user for a password; if the password is incorrect,
            // an alert is shown and the document is closed.
            string js = @"
var pwd = app.response('Enter password to view this document:', 'Password Prompt');
if (pwd != 'Secret123')
{
    app.alert('Incorrect password. The document will be closed.');
    this.closeDoc();
}
";
            // Assign the JavaScript to the document's OpenAction.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
