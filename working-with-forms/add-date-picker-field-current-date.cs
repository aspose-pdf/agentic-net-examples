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
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the field rectangle (lower‑left x, lower‑right y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

                // NOTE: In older Aspose.Pdf versions the DateTimeField class may be missing.
                // Use a TextBoxField to act as a date picker and set its default value to the current date.
                TextBoxField dateField = new TextBoxField(page, rect);
                dateField.PartialName = "DatePicker";
                // Set the default value to the current system date (format can be adjusted as needed)
                dateField.Value = DateTime.Now.ToString("yyyy-MM-dd");

                // Add the field to the page's annotations collection
                page.Annotations.Add(dateField);

                // Save the modified PDF (default PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with date picker field: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
