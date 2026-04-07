using System;
using System.Drawing; // System.Drawing.Color for DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rtl_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // (left, bottom, width, height)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(50, 700, 500, 30);

            // Create a text box field (supports user input)
            TextBoxField rtlField = new TextBoxField(page, fieldRect);
            rtlField.PartialName = "ArabicName";
            // Align text to the right to emulate right‑to‑left reading order
            rtlField.TextHorizontalAlignment = HorizontalAlignment.Right;
            // Set the default appearance: Arabic‑compatible font, size, and color
            // DefaultAppearance constructor requires System.Drawing.Color
            rtlField.DefaultAppearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);
            // Set border color (uses Aspose.Pdf.Color) and width via Border object
            rtlField.Color = Aspose.Pdf.Color.Black; // border color
            rtlField.Border = new Border(rtlField) { Width = 1 };

            // Add the field to the document's form collection
            doc.Form.Add(rtlField);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"RTL PDF form saved to '{outputPath}'.");
    }
}
