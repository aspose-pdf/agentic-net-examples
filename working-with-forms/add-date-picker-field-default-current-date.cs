using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the date picker will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Set the default value to the current system date
            dateField.Value = DateTime.Now;

            // Optional: specify the display format (default is dd/MM/yyyy)
            dateField.DateFormat = "dd/MM/yyyy";

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // Initialize the field appearance on the page
            dateField.Init(page);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}