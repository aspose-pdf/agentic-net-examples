using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and work with it inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a hidden text box field that will hold the IP address.
            // The rectangle is set to zero size because the field is hidden.
            var zeroRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            var ipField = new TextBoxField(doc.Pages[1], zeroRect)
            {
                PartialName = "UserIP",                     // field name
                Flags = AnnotationFlags.Hidden               // make the field invisible
            };

            // Add the field to the form
            doc.Form.Add(ipField);

            // Assign a document‑level JavaScript that runs when the PDF is opened.
            // Use Document.OpenAction (JavaScriptCollection does not support Add).
            doc.OpenAction = new JavascriptAction("this.getField('UserIP').value = app.getIP();");

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with hidden IP field saved to '{outputPdf}'.");
    }
}
