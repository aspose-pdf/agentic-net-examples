using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rtl_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field intended for right‑to‑left languages
            TextBoxField rtlField = new TextBoxField(page, rect)
            {
                PartialName = "ArabicField",               // Field name
                TextHorizontalAlignment = HorizontalAlignment.Right, // Align text to the right
                Value = "مثال"                              // Sample Arabic text
            };

            // Set default appearance using a font that supports Arabic glyphs
            rtlField.DefaultAppearance = new DefaultAppearance("Arial Unicode MS", 12, System.Drawing.Color.Black);

            // Add the field to the document's form collection
            doc.Form.Add(rtlField);

            // Optional: set the document language to Arabic for accessibility
            doc.TaggedContent.SetLanguage("ar");

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with RTL support saved to '{outputPath}'.");
    }
}