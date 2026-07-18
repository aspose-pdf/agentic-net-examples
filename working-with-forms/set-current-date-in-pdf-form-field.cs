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
        const string fieldName = "DateField"; // name of the placeholder field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name and cast to DateField
            var dateField = doc.Form[fieldName] as DateField;
            if (dateField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a DateField.");
            }
            else
            {
                // Optional: set a custom display format for the date (e.g., "dd MMMM yyyy")
                dateField.DateFormat = "dd MMMM yyyy";

                // Assign the current system date (DateField.Value expects a DateTime, not a string)
                dateField.Value = DateTime.Now;
            }

            // Save the updated PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
