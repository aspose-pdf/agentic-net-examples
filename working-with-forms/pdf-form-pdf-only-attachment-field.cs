using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the file‑select field
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Use a TextBoxField to let the user type (or paste) a file path.
            // FileSelectBoxField has no public constructor, so we emulate the behaviour.
            TextBoxField fileField = new TextBoxField(page, fieldRect);
            fileField.PartialName = "PdfAttachment";
            fileField.Required = true;

            // Add JavaScript validation to allow only PDF files.
            // The script runs when the field loses focus (OnValidate).
            string js = @"if (event.value != null && event.value.length > 0) {
    var ext = event.value.substr(event.value.length - 4).toLowerCase();
    if (ext != '.pdf') {
        app.alert('Only PDF files are allowed.');
        event.rc = false; // reject the value
    }
}";
            fileField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the document's form on page 1
            doc.Form.Add(fileField, 1);

            // Save the PDF form
            doc.Save("PdfAttachmentForm.pdf");
        }

        Console.WriteLine("PDF form with PDF‑only file attachment field created successfully.");
    }
}
