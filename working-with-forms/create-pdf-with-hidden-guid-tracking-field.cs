using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Generate a GUID at runtime for tracking
        string trackingGuid = Guid.NewGuid().ToString();

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the hidden text field
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a text box field on the page
            TextBoxField hiddenField = new TextBoxField(page, fieldRect)
            {
                // Set the field name (partial name)
                PartialName = "TrackingId",
                // Store the GUID as the field value
                Value = trackingGuid,
                // Mark the field as hidden using the annotation flag
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with hidden tracking field created successfully.");
    }
}