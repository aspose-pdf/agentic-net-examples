using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "DatePickerForm.pdf";

        // Ensure the output directory exists (Path.GetDirectoryName returns null when the path has no directory part)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the date picker will appear (coordinates are in points)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Aspose.Pdf older versions do not contain a dedicated DateTimeField class.
            // Use a TextBoxField to simulate a date picker – set an initial formatted date value.
            TextBoxField datePicker = new TextBoxField(page, fieldRect)
            {
                // The field name must be unique within the form
                PartialName = "DateOfBirth",

                // Set an initial value (formatted as a date)
                Value = DateTime.Now.ToString("MM/dd/yyyy")
            };

            // Set border color (the Border class does not have a Color property)
            datePicker.Color = Aspose.Pdf.Color.DarkGray;

            // Configure border using the Border class (requires the parent annotation)
            datePicker.Border = new Border(datePicker)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Set default appearance to control font name, size and text color
            datePicker.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the date picker (text box) field to the document's form collection
            doc.Form.Add(datePicker);

            // Save the PDF; the file extension determines the format (PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker form saved to '{outputPath}'.");
    }
}
