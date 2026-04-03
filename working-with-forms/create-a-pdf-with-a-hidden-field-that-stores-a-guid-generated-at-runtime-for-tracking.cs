using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for AnnotationFlags

class Program
{
    static void Main()
    {
        // Create a new PDF document
        Document doc = new Document();
        // Add a page to the document
        Page page = doc.Pages.Add();

        // Define a zero‑size rectangle for the hidden field
        var rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

        // Create a text box field that will hold the GUID
        TextBoxField hiddenField = new TextBoxField(page, rect)
        {
            PartialName = "TrackingId",
            Value = Guid.NewGuid().ToString()
        };

        // Hide the field by setting the Hidden flag directly on the field (it is itself a WidgetAnnotation)
        hiddenField.Flags = AnnotationFlags.Hidden;

        // Add the field to the document's form collection
        doc.Form.Add(hiddenField);

        // Save the PDF
        doc.Save("hidden_guid.pdf");
    }
}