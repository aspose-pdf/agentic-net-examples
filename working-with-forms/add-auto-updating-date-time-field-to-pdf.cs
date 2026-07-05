using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will appear (left, bottom, width, height)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the display format for the date and time
            dateField.DateFormat = "dd/MM/yyyy HH:mm:ss";

            // Add JavaScript that updates the field value each time the PDF is opened
            // The script uses util.printd to format the current date/time
            string js = "event.value = util.printd('dd/mm/yyyy HH:MM:ss', new Date());";
            dateField.ExecuteFieldJavaScript(new JavascriptAction(js));

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑updating date field: {outputPath}");
    }
}