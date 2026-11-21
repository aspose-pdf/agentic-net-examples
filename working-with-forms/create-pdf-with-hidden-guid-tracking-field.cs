using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "tracking.pdf";

        // Generate a GUID at runtime
        string guid = Guid.NewGuid().ToString();

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();

            // Create a hidden text box form field to store the GUID
            // Use a zero‑size rectangle since the field is not visible
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect);
            hiddenField.PartialName = "TrackingId"; // field name
            hiddenField.Value = guid;                // store the GUID
            // Hide the field from the viewer
            hiddenField.Flags = AnnotationFlags.Hidden;

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden tracking field saved to '{outputPath}'.");
    }
}
