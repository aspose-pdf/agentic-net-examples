using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_ip.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field that will store the IP address
            // Rectangle defines the field position; it can be zero‑size because the field is hidden
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField ipField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "ipHidden",                     // field name
                Flags = AnnotationFlags.Hidden                // make the field invisible
            };

            // Add the field to the form
            doc.Form.Add(ipField);

            // JavaScript that runs when the document is opened.
            // It assigns a placeholder value to the hidden field.
            // In a real scenario you could fetch the IP via an external service.
            string jsCode = @"
                var ip = '0.0.0.0'; // placeholder for client IP
                this.getField('ipHidden').value = ip;
            ";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Attach the JavaScript to the document's OpenAction (correct way)
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden IP field: {outputPath}");
    }
}
