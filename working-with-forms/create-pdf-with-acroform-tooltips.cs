using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // ---------- Text box field ----------
            // Define the field rectangle (llx, lly, urx, ury)
            var txtRect = new Rectangle(100, 600, 300, 630);

            // Create a TextBoxField on the page
            var txtField = new TextBoxField(page, txtRect)
            {
                PartialName = "CustomerName",                     // Internal field name
                AlternateName = "Enter the full name of the customer", // Tooltip shown in Acrobat
                Value = "",                                      // Initial value (empty)
                Color = Color.Black                               // Border/annotation color
            };

            // Register the field with the document's form
            doc.Form.Add(txtField);

            // ---------- Push button field ----------
            var btnRect = new Rectangle(100, 560, 200, 590);

            var btnField = new ButtonField(page, btnRect)
            {
                PartialName = "SubmitBtn",
                AlternateName = "Click to submit the form",   // Tooltip
                Color = Color.Blue
            };

            // Add the button to the form first; then set the caption.
            doc.Form.Add(btnField);
            btnField.NormalCaption = "Submit"; // Visible button label

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
