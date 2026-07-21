using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "signed_with_js.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                Name = "Signature1",
                AlternateName = "Please sign here"
            };

            // Add the signature field to the document's AcroForm collection
            // (Widgets must be added via the Form, not directly to page.Annotations)
            doc.Form.Add(sigField);

            // Attach JavaScript that runs when the user signs the field
            // This example simply shows an alert; replace with real validation logic as needed
            JavascriptAction jsAction = new JavascriptAction("app.alert('Signature validated');");
            sigField.ExecuteFieldJavaScript(jsAction);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field created: {outputPath}");
    }
}
