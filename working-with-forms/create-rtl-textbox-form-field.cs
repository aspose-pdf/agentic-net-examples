using System;
using System.Drawing; // for System.Drawing.Color used by DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Border, BorderStyle live here

class Program
{
    static void Main()
    {
        const string outputPath = "rtl_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 700, 550, 730);

            // Create a TextBoxField that will hold right‑to‑left text
            TextBoxField rtlField = new TextBoxField(page, rect)
            {
                PartialName = "RtlTextBox",                     // Field name
                Value = "مثال على النص العربي"                 // Sample Arabic text
            };

            // Set the default appearance (font, size, colour). Using a Unicode font makes Aspose render RTL text automatically.
            // DefaultAppearance expects a System.Drawing.Color, so we use the fully‑qualified System.Drawing.Color.
            rtlField.DefaultAppearance = new DefaultAppearance("Arial Unicode MS", 12, System.Drawing.Color.Black);

            // Optional: set a visible border for the field. Border requires the parent annotation in the constructor.
            rtlField.Border = new Border(rtlField)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };
            // Border colour is controlled by the annotation's Color property, which uses Aspose.Pdf.Color.
            rtlField.Color = Aspose.Pdf.Color.Black;

            // Add the field to the document's form collection
            doc.Form.Add(rtlField);

            // Save the PDF document (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"RTL PDF form saved to '{outputPath}'.");
    }
}