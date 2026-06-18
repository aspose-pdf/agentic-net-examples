using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "pdf_form_with_attachment.pdf";

        // Create a new PDF document and add a blank page
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Rectangle for the file‑input field
        Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

        // Use a TextBoxField (the public, constructible form field) to let the user specify a file name.
        // The field will later be validated with JavaScript to accept only PDF files.
        TextBoxField fileField = new TextBoxField(page, fieldRect)
        {
            PartialName = "pdfAttachment",
            AlternateName = "Attach a PDF file"
        };

        // JavaScript validation – runs when the field loses focus. It checks the entered value
        // and rejects it if the file extension is not ".pdf".
        string js = @"if (event.value) {
    var lower = event.value.toLowerCase();
    if (lower.substr(lower.length - 4) != '.pdf') {
        app.alert('Only PDF files are allowed.');
        event.rc = false; // cancel the change
    }
}";
        fileField.Actions.OnValidate = new JavascriptAction(js);

        // Add the field to the document's form collection
        doc.Form.Add(fileField);

        // Save the PDF document
        doc.Save(outputPath);

        Console.WriteLine($"PDF form with PDF‑only attachment field saved to '{outputPath}'.");
    }
}
