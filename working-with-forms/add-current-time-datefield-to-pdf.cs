using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF (can be an existing PDF or a new blank one)
        const string inputPath  = "template.pdf";
        const string outputPath = "output_with_time.pdf";

        // Ensure the input file exists; if not, create a simple one
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                // Add a single empty page
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Load the PDF document (using the recommended using pattern)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the date field will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the field (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a DateField on the selected page
            DateField dateField = new DateField(page, rect);

            // Set the display format to HH:mm:ss (24‑hour clock)
            dateField.DateFormat = "HH:mm:ss";

            // Set the default value to the current system time
            dateField.Value = DateTime.Now;

            // Optional: set a default appearance (font, size, color)
            // DefaultAppearance constructor expects System.Drawing.Color for the third argument
            dateField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with current time field at '{outputPath}'.");
    }
}