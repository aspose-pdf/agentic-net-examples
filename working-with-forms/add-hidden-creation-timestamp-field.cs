using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing the field)
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle – the field will not be visible
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a DateField on the page
            DateField timestampField = new DateField(page, hiddenRect)
            {
                // Assign a unique name to the field
                Name = "CreationTimestamp",
                // Store the current date/time (or use doc.Info.CreationDate)
                Value = DateTime.Now,
                // Prevent user editing
                ReadOnly = true
            };

            // Add the field to the document's form
            doc.Form.Add(timestampField);

            // Hide the field using a HideAction attached to a valid action property
            HideAction hide = new HideAction(timestampField.FullName, true);
            // Valid action properties include OnEnter, OnExit, OnPressMouseBtn, etc.
            timestampField.Actions.OnEnter = hide;

            // Optionally set the document's creation date metadata
            doc.Info.CreationDate = DateTime.Now;

            // Save the PDF
            doc.Save("output.pdf");
        }
    }
}
