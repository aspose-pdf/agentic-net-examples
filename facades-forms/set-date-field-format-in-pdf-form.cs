using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // TextBoxField, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "DateField"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Retrieve the specific date field. In recent Aspose.PDF versions the dedicated
            // DateTimeField class may be unavailable; a date field is represented as a TextBoxField.
            if (pdfDoc.Form != null && pdfDoc.Form[fieldName] is TextBoxField dateField)
            {
                // Set the desired date format (MM/dd/yyyy) by assigning a formatted string.
                // The Value property updates both the field's data and its visual appearance.
                string formattedDate = DateTime.Now.ToString("MM/dd/yyyy");
                dateField.Value = formattedDate; // for PDF form data and appearance
            }
            else
            {
                Console.Error.WriteLine($"Date field '{fieldName}' not found or is not a TextBoxField.");
                return;
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Date field format updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
