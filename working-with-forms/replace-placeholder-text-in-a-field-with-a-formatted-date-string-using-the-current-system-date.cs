using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a DateField on the page and add it to the form
            DateField dateField = new DateField(page, rect);
            doc.Form.Add(dateField);

            // Obtain the formatted current date using PageDate
            PageDate pageDate = new PageDate();               // uses default format dd/MM/yyyy
            string formattedDate = pageDate.GetFormattedDate(); // e.g., "27/04/2026"

            // Set the field's visible text to the formatted date
            dateField.Contents = formattedDate; // alternatively: dateField.Value = formattedDate;

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}