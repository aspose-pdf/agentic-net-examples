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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field to store the IP address.
            // A zero‑size rectangle makes the field invisible to the user.
            TextBoxField ipField = new TextBoxField(
                doc.Pages[1],
                new Rectangle(0, 0, 0, 0))
            {
                PartialName = "ipField"
                // No Flags property is required; the zero‑size rectangle hides the field.
            };

            // Add the field to the form on page 1
            doc.Form.Add(ipField, 1);

            // JavaScript that runs when the document is opened.
            // util.getHostIP() returns the client IP in Acrobat JavaScript.
            doc.OpenAction = new JavascriptAction(
                "this.getField('ipField').value = util.getHostIP();");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden IP field: {outputPath}");
    }
}
