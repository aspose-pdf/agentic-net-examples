using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Rectangle

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "datefield_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // Set the default value to the current system date
            dateField.Value = DateTime.Now;

            // Optional: specify the display format for the date
            dateField.DateFormat = "dd/MM/yyyy";

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}