using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Name of the form field that contains the placeholder text
        const string fieldName = "DatePlaceholder";

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by name. The indexer returns null if the field does not exist.
            var field = doc.Form[fieldName] as TextBoxField;
            if (field != null)
            {
                // Create a PageDate instance to obtain the formatted current date (default format dd/MM/yyyy)
                PageDate pageDate = new PageDate();
                string formattedDate = pageDate.GetFormattedDate(); // e.g., "27/08/2026"

                // Set the field value
                field.Value = formattedDate;
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a text-based field.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Placeholder replaced with date. Saved to '{outputPdf}'.");
    }
}
