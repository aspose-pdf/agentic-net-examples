using System;
using System.IO;
using System.Drawing;                     // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;                  // Form field classes

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "AcroFormAbsolute.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Define absolute positions for form fields using fully qualified
            // Aspose.Pdf.Rectangle (llx, lly, urx, ury) to avoid ambiguity.
            // -----------------------------------------------------------------
            // Text box field (e.g., Customer Name)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField txtField = new TextBoxField(doc, txtRect)
            {
                PartialName = "CustomerName",
                Value       = "John Doe"
            };
            // Set default appearance (font, size, color). The color parameter
            // must be System.Drawing.Color as required by the constructor.
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the text box to the form on page 1
            doc.Form.Add(txtField, 1);

            // -----------------------------------------------------------------
            // Additional example: a check box field positioned absolutely
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(350, 600, 370, 620);
            CheckboxField chkField = new CheckboxField(doc, chkRect)
            {
                PartialName = "Subscribe",
                // Set the checked state (true = checked)
                Value = "Yes"
            };
            // Default appearance for the check box (optional)
            chkField.DefaultAppearance = new DefaultAppearance("Helvetica", 10, System.Drawing.Color.Black);

            // Add the check box to the form on page 1
            doc.Form.Add(chkField, 1);

            // -----------------------------------------------------------------
            // Save the PDF. No SaveOptions are needed because the target format
            // is PDF (the default).
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}