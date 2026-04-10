using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";
        const string correctPassword = "Secret123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that prompts for a password on document open
            string js = $@"
var pwd = app.response('Enter password to view this document:', 'Password Required');
if (pwd == null) {{
    // User cancelled the prompt
    this.closeDoc();
}} else if (pwd != '{correctPassword}') {{
    app.alert('Incorrect password. The document will be closed.');
    this.closeDoc();
}} else {{
    // Correct password – allow viewing
    // No action needed; the document remains open
}}";

            // Assign the JavaScript as the document's OpenAction
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript security saved to '{outputPath}'.");
    }
}