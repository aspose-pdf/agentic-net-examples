using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "checkbox_checked.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the checkbox will appear
            // Fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true;               // or: checkbox.Value = "On";

            // Optionally set a name for the field (useful for form data extraction)
            checkbox.Name = "AcceptTerms";

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checked checkbox saved to '{outputPath}'.");
    }
}