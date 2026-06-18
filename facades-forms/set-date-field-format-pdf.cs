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

        try
        {
            // Load the PDF document using the Document class (instead of Form facade)
            Document pdfDocument = new Document(inputPath);

            // Retrieve the field named "Date" as a DateField
            if (pdfDocument.Form != null && pdfDocument.Form["Date"] is DateField dateField)
            {
                // Set the desired date format via the DateField's DateFormat property
                dateField.DateFormat = "MM/dd/yyyy";

                // (Optional) Set a default value using the new format
                // dateField.Value = DateTime.Now.ToString(dateField.DateFormat);
            }
            else
            {
                Console.WriteLine("Date field \"Date\" not found in the document.");
            }

            // Save the updated PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Date format updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
