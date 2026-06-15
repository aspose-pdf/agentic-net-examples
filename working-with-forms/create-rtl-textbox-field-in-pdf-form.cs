using System;
using System.Drawing; // Required for DefaultAppearance color
using Aspose.Pdf; // Core PDF classes
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // HorizontalAlignment enum

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field that supports right‑to‑left languages
            TextBoxField rtlField = new TextBoxField(page, fieldRect)
            {
                // Field name (used for form data extraction)
                PartialName = "ArabicField",

                // Use a font that contains Arabic glyphs
                // DefaultAppearance constructor requires System.Drawing.Color
                DefaultAppearance = new DefaultAppearance("Arial Unicode MS", 12, System.Drawing.Color.Black),

                // Align text to the right (common for RTL scripts)
                TextHorizontalAlignment = HorizontalAlignment.Right,

                // Sample Arabic text (optional initial value)
                Value = "مثال"
            };

            // Add the field to the form on page 1 (int page number, not Page object)
            doc.Form.Add(rtlField, 1);

            // Save the PDF document
            doc.Save("rtl_form.pdf");
        }

        Console.WriteLine("PDF form with RTL support saved as 'rtl_form.pdf'.");
    }
}