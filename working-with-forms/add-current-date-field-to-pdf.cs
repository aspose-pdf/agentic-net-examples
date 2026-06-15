using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "DateFieldOutput.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create a DateField on the page
            DateField dateField = new DateField(page, rect);

            // Set a custom date format for the field (e.g., "dd MMMM yyyy")
            dateField.DateFormat = "dd MMMM yyyy";

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Assign the current system date directly (DateField.Value expects a DateTime)
            dateField.Value = DateTime.Now;

            // Save the PDF (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date field saved to '{outputPath}'.");
    }
}
