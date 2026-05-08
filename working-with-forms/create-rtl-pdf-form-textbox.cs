using System;
using System.Drawing;                     // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;                 // For HorizontalAlignment enum

class Program
{
    static void Main()
    {
        const string outputPath = "rtl_form.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a single page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(400, 700, 550, 730);

            // Create a text box field for right‑to‑left input
            TextBoxField rtlField = new TextBoxField(page, rect);
            rtlField.PartialName   = "ArabicField";               // Internal field name
            rtlField.AlternateName = "اسم الحقل";                 // Tooltip in Arabic

            // Set default appearance using a font that supports Arabic characters
            // DefaultAppearance constructor requires System.Drawing.Color
            rtlField.DefaultAppearance = new DefaultAppearance("Arial Unicode MS", 12, System.Drawing.Color.Black);

            // Align text to the right to emulate RTL reading order (use HorizontalAlignment)
            rtlField.TextHorizontalAlignment = HorizontalAlignment.Right;

            // Add the field to the document's form on page 1 (1‑based indexing)
            doc.Form.Add(rtlField, 1);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with RTL support saved to '{outputPath}'.");
    }
}
