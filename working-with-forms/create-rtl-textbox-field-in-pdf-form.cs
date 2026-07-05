using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Rectangle

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);

            // Create a text box field for right‑to‑left language input
            TextBoxField rtlField = new TextBoxField(page, fieldRect)
            {
                PartialName = "ArabicField",               // field name
                Contents = "مثال",                         // sample Arabic text
                TextHorizontalAlignment = HorizontalAlignment.Right // align text to the right
            };

            // Set a default appearance using a font that supports Arabic characters
            // DefaultAppearance constructor takes (fontName, fontSize, Aspose.Pdf.Color)
            rtlField.DefaultAppearance = new DefaultAppearance("Arial Unicode MS", 12, System.Drawing.Color.Black);

            // Add the field to the document's form on page 1
            doc.Form.Add(rtlField, 1);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("rtl_form.pdf");
        }

        Console.WriteLine("PDF form with RTL support saved as 'rtl_form.pdf'.");
    }
}