using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "schedule_form_v3.pdf";
        const string outputPath = "location_limited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Retrieve the form field named "Location"
        if (pdfDocument.Form.HasField("Location"))
        {
            // The Form collection returns a WidgetAnnotation; cast to TextBoxField
            if (pdfDocument.Form["Location"] is TextBoxField textBox)
            {
                // Set maximum character length – the correct property name is MaxLen
                textBox.MaxLen = 12;
            }
            else
            {
                Console.Error.WriteLine("The field 'Location' exists but is not a TextBoxField.");
            }
        }
        else
        {
            Console.Error.WriteLine("The field 'Location' does not exist in the PDF.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
