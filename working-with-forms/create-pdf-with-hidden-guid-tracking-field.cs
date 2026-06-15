using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "tracking.pdf";

        // Generate a GUID at runtime for tracking
        string trackingId = Guid.NewGuid().ToString();

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle so the field is not visible
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a hidden text box field on the page
            TextBoxField hiddenField = new TextBoxField(page, hiddenRect)
            {
                // Store the GUID value
                Value = trackingId,
                // Mark the field as hidden using annotation flags
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden tracking field saved to '{outputPath}'.");
    }
}