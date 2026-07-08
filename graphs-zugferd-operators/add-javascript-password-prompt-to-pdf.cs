using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured.pdf";
        const string password   = "Secret123"; // password to validate in JavaScript

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF, add a JavaScript OpenAction that prompts for a password,
        // and save the modified document.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that asks the user for a password.
            // If the entered password does not match, the document is closed.
            string js = $"var pwd = app.response('Enter password to view this document:', 'Password', '');"
                      + $"if (pwd != '{password}') {{ this.closeDoc(); }}";

            // Attach the JavaScript to the document's OpenAction.
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF with the JavaScript embedded.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Password‑protected PDF saved to '{outputPath}'.");
    }
}