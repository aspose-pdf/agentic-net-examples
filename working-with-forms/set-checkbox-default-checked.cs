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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

            // Create a checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true; // Alternatively: checkbox.Value = "On";

            // Add the checkbox to the form fields collection
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checked checkbox saved to '{outputPath}'.");
    }
}