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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form object
            Form form = doc.Form;

            // Create a hidden text box field to store the IP address.
            // The rectangle is set to zero size because the field is hidden.
            var rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField ipField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "ipField",
                // Mark the field as hidden so it does not appear in the UI
                Flags = Aspose.Pdf.Annotations.AnnotationFlags.Hidden
            };

            // Add the field to the form
            form.Add(ipField);

            // JavaScript that runs when the document is opened.
            // It performs a synchronous HTTP GET to a public IP service
            // and stores the result in the hidden field.
            string js = @"
var xhr = new XMLHttpRequest();
xhr.open('GET', 'https://api.ipify.org?format=text', false);
xhr.send(null);
if (xhr.status == 200) {
    this.getField('ipField').value = xhr.responseText;
}
";

            // Attach the JavaScript to the document's Open action
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden IP field saved to '{outputPath}'.");
    }
}
