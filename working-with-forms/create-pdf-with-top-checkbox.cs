using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (top‑positioned)
            // Coordinates: lower‑left (50, 750), upper‑right (70, 770)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

            // Create a checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect)
            {
                // Optional: set a name and default state
                Name = "TopCheckbox",
                Checked = false
            };

            // Add the checkbox to the document's AcroForm
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save("AcroFormWithTopCheckbox.pdf");
        }

        Console.WriteLine("PDF with top‑positioned checkbox created successfully.");
    }
}