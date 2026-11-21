using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that prompts the user for a password.
            // If the entered password does not match the expected value,
            // an alert is shown and the document is closed.
            string js = @"
var pwd = app.response({
    cQuestion: 'Enter password:',
    cTitle: 'Password Required',
    bPassword: true
});
if (pwd != 'Secret123') {
    app.alert('Incorrect password. The document will be closed.');
    this.closeDoc();
}
";

            // Assign the JavaScript to the document's OpenAction so it runs when the PDF is opened
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with password prompt saved to '{outputPath}'.");
    }
}
