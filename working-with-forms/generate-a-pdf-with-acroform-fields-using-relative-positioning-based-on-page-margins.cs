using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithRelativePosition.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Retrieve page margins. If the current Aspose.Pdf version does not expose
            // MarginInfo via PageInfo, fall back to zero margins (the default).
            double leftMargin = 0;
            double bottomMargin = 0;
            try
            {
                // Some versions expose MarginInfo; use it when available.
                var marginInfo = page.PageInfo.GetType().GetProperty("MarginInfo")?.GetValue(page.PageInfo);
                if (marginInfo != null)
                {
                    leftMargin   = (double)marginInfo.GetType().GetProperty("Left").GetValue(marginInfo);
                    bottomMargin = (double)marginInfo.GetType().GetProperty("Bottom").GetValue(marginInfo);
                }
            }
            catch { /* ignore – keep defaults */ }

            // Define field size
            double fieldWidth  = 200;
            double fieldHeight = 30;

            // Position the field 50 points inside the left and bottom margins
            double fieldLeft   = leftMargin + 50;
            double fieldBottom = bottomMargin + 50;

            // Create the rectangle for the field using fully qualified type to avoid ambiguity
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(
                fieldLeft,
                fieldBottom,
                fieldLeft + fieldWidth,
                fieldBottom + fieldHeight);

            // Create a text box field (you can use other field types such as ButtonField, SignatureField, etc.)
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                Name  = "SampleTextBox",
                Value = "Enter text here"
            };

            // Add the field to the form on page 1
            doc.Form.Add(textField, 1);

            // NOTE: The DefaultAppearance class may not be present in older Aspose.Pdf versions.
            // If it exists, you can uncomment the following line; otherwise it is safely omitted.
            // doc.Form.DefaultAppearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
