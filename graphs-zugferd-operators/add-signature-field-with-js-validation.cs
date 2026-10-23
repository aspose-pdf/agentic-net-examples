using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // SignatureField and related form classes
using Aspose.Pdf.Annotations;    // JavascriptAction and other annotation actions

class Program
{
    static void Main()
    {
        const string outputPath = "signed_with_js.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the signature field
            Page page = doc.Pages.Add();

            // Define the signature field rectangle (llx, lly, urx, ury) using fully qualified type
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create the signature field on the page
            SignatureField signatureField = new SignatureField(page, sigRect)
            {
                PartialName = "Signature1"   // internal name of the field
            };

            // Attach JavaScript that runs when the signature is validated by the user
            // The OnValidate action is triggered after the user signs the field
            signatureField.Actions.OnValidate = new JavascriptAction(
                "app.alert('Signature validated successfully');");

            // Add the signature field to the document's interactive form
            doc.Form.Add(signatureField);

            // Save the PDF; Document.Save without SaveOptions writes a PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field and JavaScript saved to '{outputPath}'.");
    }
}
