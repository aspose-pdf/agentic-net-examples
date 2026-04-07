using System;
using System.IO;
using Aspose.Pdf;               // Required for Document and Page
using Aspose.Pdf.Forms;        // Required for DateField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define the rectangle where the date field will be placed
            // (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);

            // Create a DateField on the specified page and rectangle
            Aspose.Pdf.Forms.DateField dateField = new Aspose.Pdf.Forms.DateField(page, rect);

            // Set the display format to show only the time (HH:mm:ss)
            dateField.DateFormat = "HH:mm:ss";

            // Set the default value to the current system time
            dateField.Value = DateTime.Now;

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Initialize the field (required for proper appearance)
            dateField.Init(page);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with time field: '{outputPath}'.");
    }
}