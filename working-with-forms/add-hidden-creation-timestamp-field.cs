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

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing the field)
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle; the field will be invisible on the page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a DateField to store the creation timestamp
            DateField timestampField = new DateField(page, rect);
            timestampField.Name = "CreationTimestamp";          // field name
            timestampField.Value = DateTime.Now;                // store current time
            timestampField.Flags = AnnotationFlags.Hidden;      // hide the field from the UI

            // Add the field to the document's form
            doc.Form.Add(timestampField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden timestamp saved to '{outputPath}'.");
    }
}