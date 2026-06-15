using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Fill form fields – cast the returned WidgetAnnotation to a Field
                Field? nameField = doc.Form["Name"] as Field;
                if (nameField != null)
                    nameField.Value = "John Doe";

                Field? dateField = doc.Form["Date"] as Field;
                if (dateField != null)
                    dateField.Value = DateTime.Today.ToShortDateString();

                // Flatten the form – removes interactive fields and writes their values onto the page
                doc.Form.Flatten();

                // Save the flattened PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
