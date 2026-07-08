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
            // Set the document creation date metadata (must be a local time value)
            // Aspose.Pdf expects a DateTime with Kind = Local; using DateTime.Now satisfies this requirement.
            doc.Info.CreationDate = DateTime.Now;

            // Add a blank page – the field must be attached to a page
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle so the field is not visible on the page
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a DateField to store the timestamp
            DateField timestampField = new DateField(page, hiddenRect)
            {
                // Give the field a unique name
                PartialName = "CreationTimestamp",
                // Store the current UTC time (the field value can be UTC even if the metadata is local)
                Value = DateTime.UtcNow,
                // Make the field read‑only so it cannot be edited
                ReadOnly = true,
                // Hide the field using the Hidden flag
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form
            doc.Form.Add(timestampField);

            // Save the PDF
            doc.Save("output.pdf");
        }
    }
}
