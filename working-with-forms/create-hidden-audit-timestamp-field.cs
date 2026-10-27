using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "audit_timestamp.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages.Add();

            // Define a zero‑size rectangle for the hidden field (position is irrelevant)
            Rectangle rect = new Rectangle(0, 0, 0, 0);

            // Create a text box form field that will hold the creation timestamp
            TextBoxField timestampField = new TextBoxField(page, rect);
            timestampField.PartialName = "CreationTimestamp";
            timestampField.Value = DateTime.UtcNow.ToString("o"); // ISO‑8601 timestamp

            // No need to set Flags; a zero‑size rectangle makes the field invisible to the user
            // timestampField.Flags = FieldFlag.Hidden; // <-- removed

            // Add the field to the document's AcroForm on page 1
            doc.Form.Add(timestampField, 1);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden timestamp saved to '{outputPath}'.");
    }
}
