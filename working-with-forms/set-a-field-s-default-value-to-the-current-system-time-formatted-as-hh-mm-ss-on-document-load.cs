using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_time.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 200, 730);

            // Create a new DateField on the first page
            DateField dateField = new DateField(doc, fieldRect);

            // Set the display format to HH:mm:ss
            dateField.DateFormat = "HH:mm:ss";

            // Set the field's default value to the current system time
            dateField.Value = DateTime.Now;

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with date field: {outputPath}");
    }
}