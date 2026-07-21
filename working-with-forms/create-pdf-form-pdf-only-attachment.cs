using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "pdf_form_with_attachment.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the file‑attachment field will be placed
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Aspose.Pdf does not expose a public constructor for FileSelectBoxField.
            // Use a TextBoxField to let the user type (or paste) a file name and validate it with JavaScript.
            TextBoxField fileField = new TextBoxField(page, fieldRect)
            {
                PartialName   = "PdfAttachment",
                AlternateName = "Attach a PDF file",
                Required      = true
                // Tooltip property is not supported on TextBoxField in the current API version.
            };

            // JavaScript that runs on validation and allows only *.pdf files.
            string js = @"
if (event.value != null && event.value.length > 0) {
    var lower = event.value.toLowerCase();
    if (!lower.endsWith('.pdf')) {
        app.alert('Only PDF files are allowed.');
        event.rc = false; // reject the value
    }
}";
            fileField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the document's form collection
            doc.Form.Add(fileField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with PDF‑only attachment field saved to '{outputPath}'.");
    }
}
