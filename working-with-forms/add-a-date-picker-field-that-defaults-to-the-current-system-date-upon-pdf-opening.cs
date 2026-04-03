using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Set the default value to the current system date
            dateField.Value = DateTime.Now;

            // Optional: set a custom display format (default is "dd/MM/yyyy")
            // dateField.DateFormat = "MM/dd/yyyy";

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // Initialize the field (required for proper appearance)
            dateField.Init(page);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with date picker field: {outputPath}");
    }
}