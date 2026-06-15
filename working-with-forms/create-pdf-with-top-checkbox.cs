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
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox positioned near the top of the page
            // Constructor: Rectangle(llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

            // Create the checkbox field on the specified page and rectangle
            CheckboxField checkbox = new CheckboxField(page, rect)
            {
                Name = "TopCheckbox",   // Field name used in form data
                ExportValue = "Yes",    // Value exported when the box is checked
                Checked = false         // Initial state (unchecked)
            };

            // Add the checkbox to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with top-positioned checkbox saved to '{outputPath}'.");
    }
}