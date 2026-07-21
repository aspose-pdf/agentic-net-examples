using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "audit_timestamp.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the hidden field will reside (coordinates are in points)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0); // zero‑size field, invisible

            // Create a DateField on the specified page with the defined rectangle
            DateField timestampField = new DateField(page, fieldRect)
            {
                // Set a unique name for the field
                Name = "CreationTimestamp",
                // Store the current date and time
                Value = DateTime.Now,
                // Mark the field as hidden so it does not appear in the UI
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form
            doc.Form.Add(timestampField);

            // Optionally set the document's creation date metadata
            doc.Info.CreationDate = DateTime.Now;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden audit timestamp saved to '{outputPath}'.");
    }
}
