using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Core Forms API (no Facades)

class Program
{
    static void Main()
    {
        const string outputPath = "XfaFlattened.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();

            // ---------------------------------------------------------------------
            // NOTE: The current Aspose.Pdf package version does not contain the
            //       Aspose.Pdf.Xfa namespace.  To keep the example functional and
            //       still demonstrate "form creation + flattening" we use the core
            //       AcroForm API (Aspose.Pdf.Forms).  This satisfies the original
            //       intent – a form field that is later flattened to static content –
            //       while staying within the allowed namespaces.
            // ---------------------------------------------------------------------

            // Define a rectangle for the text field (left, bottom, right, top)
            var fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field named "Name" with a default value
            var nameField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "Name",
                Value = "John Doe"
            };

            // Add the field to the document's AcroForm collection
            doc.Form.Add(nameField);

            // Flatten the form so that fields become static PDF content
            doc.Form.Flatten();

            // Save the resulting PDF (extension is ignored, Save writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form fields flattened and saved to '{outputPath}'.");
    }
}
