using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the field on
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);

            // Create a DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Set the display format to HH:mm:ss
            dateField.DateFormat = "HH:mm:ss";

            // Set the field's default value to the current system time
            dateField.Value = DateTime.Now;

            // Optional: define the field's visual appearance (font, size, color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            dateField.DefaultAppearance = appearance;

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Initialize the JavaScript action required for date fields
            dateField.Init(page);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with date field to '{outputPath}'.");
    }
}