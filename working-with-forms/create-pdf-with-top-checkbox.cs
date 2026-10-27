using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for Border class

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithCheckbox.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (default size A4)
            Page page = doc.Pages.Add();

            // Define a rectangle for the checkbox positioned near the top of the page.
            // PDF coordinate origin is bottom‑left, so we subtract from page height.
            // Rectangle(left, lower, right, upper)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                llx: 50,                         // left margin
                lly: page.PageInfo.Height - 70, // lower Y (70 points below top)
                urx: 70,                         // right margin (20‑point width)
                ury: page.PageInfo.Height - 50  // upper Y (50 points below top)
            );

            // Create the checkbox field. The constructor takes (Page, Rectangle).
            CheckboxField checkBox = new CheckboxField(page, rect)
            {
                // The field's name (PartialName) is set separately.
                Checked = false,                 // default state (unchecked)
                Color = Color.LightGray          // background‑like colour (optional)
            };
            checkBox.PartialName = "TopCheckBox";

            // Set the visual border. Border requires the parent annotation in the constructor.
            checkBox.Border = new Border(checkBox)
            {
                Width = 1
            };
            // Border colour is controlled by the annotation's own Color property.
            checkBox.Color = Color.Black;

            // Add the field to the document's AcroForm
            doc.Form.Add(checkBox);

            // Save the PDF. The using block ensures the document stays alive until Save completes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm and top‑positioned checkbox saved to '{outputPath}'.");
    }
}
