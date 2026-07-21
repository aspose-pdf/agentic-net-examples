using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field on the first page.
            // A zero‑size rectangle makes the field invisible.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField ipField = new TextBoxField(doc.Pages[1], rect);
            ipField.PartialName = "UserIP";

            // Add the field to the form.
            doc.Form.Add(ipField);

            // JavaScript that runs when the document is opened.
            // It should set the field value to the user's IP address.
            // (Actual IP retrieval must be implemented on the client side or via a service.)
            string js = @"
                // TODO: replace with real IP retrieval logic.
                var ip = '';
                this.getField('UserIP').value = ip;
            ";

            // Attach the JavaScript to the document's OpenAction (read‑only property).
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden IP field saved to '{outputPath}'.");
    }
}
