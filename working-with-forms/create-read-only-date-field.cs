using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // Create a new PDF document (no external file required)
        using (Document doc = new Document())
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Add();

            // Define the rectangle for the date field (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the DateField on the first page and configure it
            DateField dateField = new DateField(page, rect)
            {
                // Initial value – today’s date
                Value = DateTime.Today,
                // Desired display format
                DateFormat = "MM/dd/yyyy",
                // Make the field read‑only so the user cannot modify it later
                ReadOnly = true
            };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Date field created as read‑only and saved to '{outputPdf}'.");
    }
}
