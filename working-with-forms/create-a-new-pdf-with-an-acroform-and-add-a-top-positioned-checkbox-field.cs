using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_top.pdf";

        // Create a new PDF document (initially contains no pages)
        using (Document doc = new Document())
        {
            // Add a blank page to the document and obtain a reference to it
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox positioned near the top of the page
            // (llx, lly, urx, ury) – 20×20 points size, 50 points from the left edge,
            // 750 points from the bottom edge (approximately top of a standard page)
            Rectangle rect = new Rectangle(50, 750, 70, 770);

            // Create the checkbox field on the specified page and rectangle
            CheckboxField checkbox = new CheckboxField(page, rect)
            {
                // Optional: assign a field name and export value
                Name = "TopCheck",
                ExportValue = "Yes",
                // Set the initial state (unchecked)
                Checked = false
            };

            // Add the checkbox to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with top‑positioned checkbox saved to '{outputPath}'.");
    }
}
