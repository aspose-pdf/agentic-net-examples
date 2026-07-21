using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF to load
        const string outputPath = "filled.pdf";     // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will be placed
            // (left, bottom, right, top) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);

            // Create a DateField on the document
            DateField dateField = new DateField(doc, rect);

            // Set the display format to HH:mm:ss
            dateField.DateFormat = "HH:mm:ss";

            // Set the default value to the current system time
            dateField.Value = DateTime.Now;

            // Optional: set the visual appearance (font, size, color)
            dateField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}