using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "DatePickerForm.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add first page (1‑based indexing)
            doc.Pages.Add();

            // Define the rectangle where the date picker will appear
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a DateField on the first page at the specified rectangle
            DateField dateField = new DateField(doc.Pages[1], rect)
            {
                // Set a unique name for the field (used to retrieve its value later)
                Name = "DateField",
                PartialName = "DateField",

                // Optional: define the display format of the date
                DateFormat = "MM/dd/yyyy"
            };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF (Document disposal is handled by the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker field saved to '{outputPath}'.");
    }
}