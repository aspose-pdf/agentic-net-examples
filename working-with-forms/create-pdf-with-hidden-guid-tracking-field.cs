using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Path for the generated PDF
        const string outputPath = "tracking.pdf";

        // Generate a GUID at runtime
        string trackingGuid = Guid.NewGuid().ToString();

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing form fields)
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle for the hidden field
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a text box field that will hold the GUID
            TextBoxField hiddenField = new TextBoxField(page, fieldRect)
            {
                Name = "TrackingId",          // Field name
                Value = trackingGuid,         // Store the GUID
                ReadOnly = true               // Prevent user edits
            };

            // Mark the field as hidden using the annotation flag (enum assignment, not int)
            hiddenField.Flags = AnnotationFlags.Hidden;

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF (no explicit SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden tracking field saved to '{outputPath}'.");
    }
}
