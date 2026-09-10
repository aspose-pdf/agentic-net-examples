using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (default size)
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (positioned near the top of the page)
            // Rectangle(left, bottom, right, top) – coordinates are in points (1/72 inch)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

            // Create the checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, chkRect)
            {
                Name = "TopCheckbox",   // field name
                Checked = true          // default state (checked)
            };

            // Add the checkbox to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save("AcroFormWithTopCheckbox.pdf");
        }

        Console.WriteLine("PDF with top‑positioned checkbox created successfully.");
    }
}