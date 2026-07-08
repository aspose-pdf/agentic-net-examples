using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "checkbox_checked.pdf";

        // Create a new PDF document (lifecycle: create)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            // Two equivalent ways:
            // 1) Using the boolean property
            checkbox.Checked = true;
            // 2) Or using the Value property with the "On" state name
            // checkbox.Value = "On";

            // Optionally set a name and export value
            checkbox.Name = "AgreeTerms";
            checkbox.ExportValue = "Yes";

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checked checkbox saved to '{outputPath}'.");
    }
}