using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // existing PDF
        const string outputPath = "output.pdf";     // PDF with date/time field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the field will appear (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect)
            {
                PartialName = "CurrentDateTime",          // field name (used in JavaScript)
                DateFormat  = "dd/MM/yyyy HH:mm:ss",     // display format
                Value       = DateTime.Now                // initial value
            };

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // JavaScript to update the field each time the PDF is opened
            // The script sets the field's value to the current date/time of the viewer
            string jsCode = "this.getField('CurrentDateTime').value = new Date().toString();";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Attach the script to the document's OpenAction (executed on each view)
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑updating date/time field saved to '{outputPath}'.");
    }
}
