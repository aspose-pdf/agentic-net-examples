using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Date"
            // The field collection returns a generic FormField, so cast to DateField
            Aspose.Pdf.Forms.DateField dateField = doc.Form["Date"] as Aspose.Pdf.Forms.DateField;

            if (dateField == null)
            {
                Console.Error.WriteLine("Date field not found or is not a DateField.");
                return;
            }

            // Set the desired display format for the date field
            // The format string follows .NET custom date format patterns
            dateField.DateFormat = "MM/dd/yyyy";

            // Optionally set a value to verify the format (e.g., current date)
            dateField.Value = DateTime.Now;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date field format updated and saved to '{outputPath}'.");
    }
}