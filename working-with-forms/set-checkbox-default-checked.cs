using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_checked.pdf";

        // Document lifecycle must be managed with using (rule: document-disposal-with-using)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Fully qualified rectangle to avoid ambiguity (rule: rectangle-disambiguation)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a checkbox field on the page (constructor from CheckboxField(Page, Rectangle))
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked (Checked property)
            checkbox.Checked = true;

            // Optional: define the export value that will be submitted with the form
            checkbox.ExportValue = "Yes";

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF (rule: use Document.Save within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}