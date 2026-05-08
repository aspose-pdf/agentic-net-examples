using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // Define the rectangle where the date field will be placed (coordinates are in points)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a DateField on the first page
            var dateField = new DateField(doc.Pages[1], rect)
            {
                // Optional: set the desired date format for the field
                DateFormat = "dd/MM/yyyy",
                // Assign the current system date (DateTime, not string)
                Value = DateTime.Now
            };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated date field: '{outputPath}'.");
    }
}
