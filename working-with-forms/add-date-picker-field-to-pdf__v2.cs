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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the date picker field
            // Rectangle(llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the default value to the current system date
            dateField.Value = DateTime.Now;

            // Optional: specify the display format (default is dd/MM/yyyy)
            dateField.DateFormat = "dd/MM/yyyy";

            // Add the field to the form and initialize it on the page
            doc.Form.Add(dateField);
            dateField.Init(doc.Pages[1]);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added. Saved to '{outputPath}'.");
    }
}