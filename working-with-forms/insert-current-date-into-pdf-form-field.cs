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
            // Define the rectangle where the date field will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create a DateField on the document using the rectangle
            DateField dateField = new DateField(doc, rect)
            {
                // Optional: set the display format of the date field
                DateFormat = "dd/MM/yyyy"
            };

            // Set the field's value to the current system date (DateTime)
            dateField.Value = DateTime.Now;

            // Add the field to the PDF form
            doc.Form.Add(dateField);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
