using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Generate a GUID at runtime for tracking
        string trackingId = Guid.NewGuid().ToString();

        // Create a new PDF document (wrapped in using as per lifecycle rule)
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing form fields)
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle for the hidden field (position is irrelevant)
            Rectangle fieldRect = new Rectangle(0, 0, 0, 0);

            // Create a textbox field on the page, assign a partial name and store the GUID as its value
            TextBoxField hiddenField = new TextBoxField(page, fieldRect)
            {
                PartialName = "TrackingId",
                Value = trackingId
                // No need to set a flag; the zero‑size rectangle keeps the field invisible
            };

            // Add the field to the document's form
            doc.Form.Add(hiddenField);

            // Save the PDF
            doc.Save("HiddenGuid.pdf");
        }
    }
}
