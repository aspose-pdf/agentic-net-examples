using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define 1‑inch margins (72 points = 1 inch)
            double marginLeft   = 72;
            double marginRight  = 72;
            double marginTop    = 72;
            double marginBottom = 72;

            // Apply margins to the page
            page.PageInfo.Margin = new MarginInfo(marginLeft, marginRight, marginTop, marginBottom);

            // Define field dimensions
            double fieldWidth  = 200; // points
            double fieldHeight = 20;  // points

            // Position the field relative to the margins
            // Example: 0.5 inch (36 points) from left margin and 0.5 inch from bottom margin
            double offsetX = 36;
            double offsetY = 36;

            double llx = page.PageInfo.Margin.Left + offsetX;
            double lly = page.PageInfo.Margin.Bottom + offsetY;
            double urx = llx + fieldWidth;
            double ury = lly + fieldHeight;

            // Create the rectangle for the field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a text box field placed on the document (constructor accepts Document)
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "SampleText",
                Value       = "Enter text here"
            };

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Add an additional appearance on the same page (optional but demonstrates AddFieldAppearance)
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}