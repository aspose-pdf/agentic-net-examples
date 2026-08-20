using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // DateField resides here

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Attempt to retrieve the field named "Date"
            // The Form indexer returns a generic Field; cast to DateField to access DateFormat
            var field = doc.Form["Date"];
            if (field is DateField dateField)
            {
                // Set the desired date format
                dateField.DateFormat = "MM/dd/yyyy";

                // Optional: set a default appearance (font, size, color) if needed
                // DefaultAppearance ctor expects System.Drawing.Color for the third argument
                // Uncomment and adjust as required:
                // dateField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            }
            else
            {
                Console.WriteLine("Date field not found or is not a DateField.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}