using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API
        Document pdfDocument = new Document(inputPath);

        // Retrieve the form field named "Country"
        // Document.Form indexer returns a WidgetAnnotation, so cast it to a Field first
        Field? field = pdfDocument.Form["Country"] as Field;
        if (field is ComboBoxField comboBox)
        {
            // Set the default selected item to "United States"
            comboBox.Value = "United States";
        }
        else
        {
            Console.Error.WriteLine("Field 'Country' not found or is not a combo box.");
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
