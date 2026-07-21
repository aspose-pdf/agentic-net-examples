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
        const string outputPath = "output_with_date.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will appear (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the display format for the date and time
            dateField.DateFormat = "dd/MM/yyyy HH:mm:ss";

            // Initialize the field with the current date and time
            dateField.Value = DateTime.Now;

            // Add JavaScript that updates the field each time the document is opened or the field is viewed
            dateField.ExecuteFieldJavaScript(
                new JavascriptAction("event.value = new Date().toLocaleString();")
            );

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑updating date field: {outputPath}");
    }
}