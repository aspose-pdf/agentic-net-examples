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

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Date" safely
            Field? field = doc.Form["Date"] as Field;
            if (field is DateField dateField)
            {
                // Set the desired date format (appearance option)
                dateField.DateFormat = "MM/dd/yyyy";
                Console.WriteLine("Date field format updated to MM/dd/yyyy.");
            }
            else
            {
                Console.WriteLine("Date field not found or is not a DateField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
