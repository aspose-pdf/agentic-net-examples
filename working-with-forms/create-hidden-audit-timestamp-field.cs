using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "audit_timestamp.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page (required for the AcroForm to exist).
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle for the hidden field (its visual area is irrelevant).
            Rectangle rect = new Rectangle(0, 0, 0, 0);

            // Create a hidden text box field to store the creation timestamp.
            TextBoxField timestampField = new TextBoxField(page, rect)
            {
                // Field name (must be unique within the form).
                PartialName = "CreationTimestamp",

                // Set the field value to the current UTC timestamp.
                Value = DateTime.UtcNow.ToString("o")
            };

            // Mark the field as hidden so it does not appear in the UI.
            // The hidden flag is set via the AnnotationFlags enumeration.
            timestampField.Flags = AnnotationFlags.Hidden;

            // Add the hidden field to the form on page 1.
            doc.Form.Add(timestampField, 1);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden audit timestamp saved to '{outputPath}'.");
    }
}
