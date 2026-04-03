using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page (required for field placement)
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a hidden text box field to store the IP address.
            // The rectangle is set to zero size so the field is not visible.
            TextBoxField ipField = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(0, 0, 0, 0)   // zero‑size rectangle → invisible
            )
            {
                PartialName = "HiddenIP",               // field name
                Value       = string.Empty               // initial value
            };

            // Add the field to the form
            doc.Form.Add(ipField);

            // JavaScript that runs when the document is opened.
            // It attempts to retrieve the client IP address (via a placeholder function)
            // and stores it in the hidden field.
            // Note: Actual IP retrieval depends on the PDF viewer's capabilities.
            string js = @"
                try {
                    // Placeholder: replace with a real method to obtain the IP address.
                    var ip = app.getIP ? app.getIP() : '0.0.0.0';
                    this.getField('HiddenIP').value = ip;
                } catch (e) { /* ignore errors */ }
            ";

            // Attach the JavaScript to the document's OpenAction.
            // Document.OpenAction expects a single JavascriptAction.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with hidden IP field saved to '{outputPdf}'.");
    }
}
