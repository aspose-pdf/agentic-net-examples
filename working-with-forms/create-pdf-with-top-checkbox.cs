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
            // Add a single page (default size)
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox near the top of the page
            // Rectangle constructor: (llx, lly, urx, ury)
            // Here we place it at (50, 770) with a size of 20x20 points
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(50, 770, 70, 790);

            // Create the checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, checkboxRect);

            // Assign a name to the field (used for form data)
            checkbox.PartialName = "TopCheckbox";

            // Set the initial state (checked by default)
            checkbox.Checked = true;

            // Add the field to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with top‑positioned checkbox saved to '{outputPath}'.");
    }
}