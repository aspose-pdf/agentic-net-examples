using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set a name for the field (used to reference it later)
            dateField.Name = "DatePicker";

            // Define the display format for the date (e.g., mm/dd/yyyy)
            dateField.DateFormat = "mm/dd/yyyy";

            // Attach JavaScript that opens the calendar picker when the field is activated
            // The script uses the built‑in AFDate_FormatEx function to format the selected date
            dateField.OnActivated = new JavascriptAction("AFDate_FormatEx('mm/dd/yyyy');");

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}