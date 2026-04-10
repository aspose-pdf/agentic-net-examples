using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "FileAttachmentForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the file input box field (llx, lly, urx, ury)
            var fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a TextBoxField that will hold the file name/path.
            // Aspose.Pdf does not expose a public constructor for FileSelectBoxField, so we use a TextBoxField
            // and add JavaScript validation to restrict the user to PDF files only.
            TextBoxField fileField = new TextBoxField(page, fieldRect)
            {
                PartialName = "PdfAttachmentField",
                AlternateName = "Attach PDF File",
                Required = true
            };

            // JavaScript that validates the entered value ends with ".pdf" (case‑insensitive).
            // If the validation fails, the field will reject the input and show an alert.
            string jsValidate = @"
                var val = event.value;
                if (val == null || val.length == 0) {
                    // Empty value – let the Required flag handle it later.
                    event.rc = true;
                } else {
                    var lower = val.toLowerCase();
                    if (lower.endsWith('.pdf')) {
                        event.rc = true; // Accept the value
                    } else {
                        app.alert('Only PDF files are allowed.');
                        event.rc = false; // Reject the value
                    }
                }
            ";
            fileField.Actions.OnValidate = new JavascriptAction(jsValidate);

            // Add the field to the document's form collection
            doc.Form.Add(fileField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with file attachment field saved to '{outputPath}'.");
    }
}
