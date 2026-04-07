using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "tracking.pdf";

        // Generate a GUID at runtime for tracking
        string guid = Guid.NewGuid().ToString();

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for form fields)
            Page page = doc.Pages.Add();

            // Define a rectangle for the hidden field (size can be zero)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a textbox field to store the GUID
            TextBoxField hiddenField = new TextBoxField(page, rect);
            hiddenField.PartialName = "TrackingId";   // field name
            hiddenField.Value = guid;                // store GUID
            hiddenField.ReadOnly = true;             // prevent editing

            // Mark the field as hidden
            hiddenField.Flags = AnnotationFlags.Hidden;

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden tracking field saved to '{outputPath}'.");
    }
}