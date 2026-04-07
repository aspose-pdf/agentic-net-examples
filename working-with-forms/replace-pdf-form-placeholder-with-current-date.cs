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
        const string fieldName  = "DateField"; // name of the form field to update

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name
            var field = doc.Form[fieldName];
            if (field != null)
            {
                // Get the current date formatted according to the default PageDate format
                string formattedDate = new PageDate().GetFormattedDate();

                // Update the field's displayed text
                field.Contents = formattedDate;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Placeholder replaced with date. Saved to '{outputPath}'.");
    }
}