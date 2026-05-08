using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected_output.pdf";
        const string password   = "mySecret"; // password to validate in JavaScript

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that runs when the document is opened.
            // It prompts the user for a password and closes the document if the password is incorrect.
            string js = $"var pwd = app.response('Enter password to view this document:', 'Password');"
                      + $"if (pwd != '{password}') {{"
                      + "app.alert('Incorrect password. The document will be closed.');"
                      + "this.closeDoc();"
                      + "}}";

            // Assign the JavaScript to the document's OpenAction
            doc.OpenAction = new JavascriptAction(js);

            // Encrypt the PDF as an additional layer of security
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript password prompt to '{outputPath}'.");
    }
}