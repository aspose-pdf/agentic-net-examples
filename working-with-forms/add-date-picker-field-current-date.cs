using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Set the default value to the current system date
            dateField.Value = DateTime.Now;

            // Optional: specify the display format (default is dd/MM/yyyy)
            dateField.DateFormat = "dd/MM/yyyy";

            // Add the field to the document's form and initialize it
            doc.Form.Add(dateField);
            dateField.Init(page);

            // Save the modified PDF (lifecycle rule: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added and saved to '{outputPath}'.");
    }
}