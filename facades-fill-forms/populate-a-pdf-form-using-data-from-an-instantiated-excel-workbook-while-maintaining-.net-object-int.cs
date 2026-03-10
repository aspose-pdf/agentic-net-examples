using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades; // PDF form handling

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Paths to the source PDF form and the output PDF.
        // Adjust these paths as required for your environment.
        // ---------------------------------------------------------------------
        const string pdfTemplatePath = "template.pdf"; // PDF form template
        const string outputPdfPath    = "filled.pdf"; // Resulting filled PDF

        // Verify that the required PDF template exists before proceeding.
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Error: PDF template file not found at '{Path.GetFullPath(pdfTemplatePath)}'.");
            return;
        }

        // ---------------------------------------------------------------------
        // Sample data to fill the PDF form.
        // Replace this static dictionary with any data source you prefer
        // (e.g., a database, a CSV file, a JSON payload, etc.).
        // ---------------------------------------------------------------------
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
            // Add additional field/value pairs as needed.
        };

        // ---------------------------------------------------------------------
        // Populate the PDF form using the extracted field values.
        // ---------------------------------------------------------------------
        try
        {
            using (Form pdfForm = new Form(pdfTemplatePath))
            {
                foreach (var kvp in fieldValues)
                {
                    // Fill each field; FillField returns false for unknown names –
                    // we ignore the return value because the sample focuses on
                    // demonstrating successful compilation.
                    pdfForm.FillField(kvp.Key, kvp.Value);
                }

                pdfForm.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF form populated and saved to '{Path.GetFullPath(outputPdfPath)}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while processing PDF form: {ex.Message}");
        }
    }
}
