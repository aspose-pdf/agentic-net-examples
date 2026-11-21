using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page (default size A4).
            Page page = doc.Pages.Add();

            // Determine the rectangle for the checkbox positioned near the top of the page.
            // Coordinates: (llx, lly) lower‑left, (urx, ury) upper‑right.
            // Place the checkbox 50 points from the left edge and 50 points from the top edge.
            double checkboxWidth = 20;
            double marginLeft = 50;
            double marginTop = 50;
            double llx = marginLeft;
            double urx = marginLeft + checkboxWidth;
            double ury = page.PageInfo.Height - marginTop;
            double lly = ury - checkboxWidth; // square checkbox

            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the checkbox field. Use the (Page, Rectangle) constructor and set the name via PartialName.
            CheckboxField checkBox = new CheckboxField(page, rect);
            checkBox.PartialName = "TopCheckBox"; // field name
            checkBox.Checked = false; // initially unchecked

            // Add the field to the document's AcroForm.
            doc.Form.Add(checkBox);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm checkbox saved to '{outputPath}'.");
    }
}
