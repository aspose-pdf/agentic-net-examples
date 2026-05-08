using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "hidden_guid.pdf";

        // Generate a GUID at runtime for tracking
        string guid = Guid.NewGuid().ToString();

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (required for placing a form field)
            Page page = doc.Pages.Add();

            // Define a rectangle for the hidden field.
            // Position and size are irrelevant because the field will be hidden.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a textbox form field on the page
            TextBoxField hiddenField = new TextBoxField(page, rect)
            {
                // Set a logical name for the field
                PartialName = "TrackingId",
                // Store the generated GUID as the field value
                Value = guid,
                // Mark the field as hidden so it does not appear in the PDF viewer
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden GUID saved to '{outputPath}'.");
    }
}