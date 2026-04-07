using System;
using System.IO;
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
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define a rectangle positioned near the top of the page
            // Constructor: Rectangle(llx, lly, urx, ury) – expects float values
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50f, 730f, 70f, 750f);

            // Create a checkbox field on the page using the defined rectangle
            CheckboxField checkbox = new CheckboxField(page, rect)
            {
                Name = "TopCheckbox",   // Optional field name
                Checked = false         // Initial state (unchecked)
            };

            // Add the checkbox to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with top‑positioned checkbox saved to '{outputPath}'.");
    }
}
