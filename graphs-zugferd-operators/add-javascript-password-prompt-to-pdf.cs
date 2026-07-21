using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected_output.pdf";
        const string password   = "secret"; // password to validate against

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // JavaScript executed when the document is opened.
            // It prompts the user for a password and closes the document if the password is incorrect.
            string js = $"var pwd = app.response('Enter password:', 'Password');" +
                        $"if (pwd != '{password}') {{ " +
                        "app.alert('Incorrect password. Document will be closed.'); " +
                        "this.closeDoc(); " +
                        "}}";

            // Assign the JavaScript action to the document's OpenAction.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF (lifecycle rule: use provided save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript password prompt: {outputPath}");
    }
}