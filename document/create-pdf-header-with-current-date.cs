using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "HeaderWithDate.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (more pages can be added later)
            Page page = doc.Pages.Add();

            // Define a rectangle for the header date field (positioned at the top)
            // Coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            // Here we place the field 50 points from the left edge and 30 points from the top edge.
            double pageHeight = page.PageInfo.Height;
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(50, pageHeight - 30, 200, pageHeight - 10);

            // Create a DateField (a form field that shows the current date)
            // The DateField internally uses JavaScript to populate its value when the PDF is opened.
            DateField dateField = new DateField(page, dateRect);
            doc.Form.Add(dateField);

            // Make the field shared so the same header appears on every page
            dateField.IsSharedField = true;

            // Optional: set the date format (e.g., "MM/dd/yyyy")
            dateField.DateFormat = "MM/dd/yyyy";

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with header date created: {outputPath}");
    }
}