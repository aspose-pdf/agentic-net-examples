using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string password   = "Secret123";   // password to check against

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that prompts the user for a password when the document is opened.
            // If the entered password does not match the expected one, the document is closed.
            string js = $@"
var pwd = app.response('Enter password to view the document:', 'Password Prompt');
if (pwd == null) {{ this.closeDoc(); }}
else if (pwd != '{password}') {{
    app.alert('Incorrect password. The document will be closed.');
    this.closeDoc();
}}";

            // Assign the JavaScript as the document's OpenAction
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
    }
}