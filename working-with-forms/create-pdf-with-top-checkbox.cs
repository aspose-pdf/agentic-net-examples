using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_top.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the checkbox rectangle positioned near the top of the page
            // Rectangle constructor: (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(50, 730, 70, 750);

            // Create the checkbox field on the page
            var checkbox = new CheckboxField(page, rect)
            {
                PartialName = "TopCheckbox", // field name
                Checked = false               // default unchecked state
            };

            // Add the checkbox to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with top‑positioned checkbox saved to '{outputPath}'.");
    }
}
