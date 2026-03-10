using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades; // Form class for PDF form operations

class Program
{
    static void Main()
    {
        const string templatePdf = "template.pdf";   // PDF with AcroForm fields
        const string outputDir   = "FilledForms";    // Directory for generated PDFs

        // Validate input files
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Sample data source – replace with any data provider (CSV, DB, etc.)
        // Each dictionary represents one row of data where the key matches a PDF
        // form field name.
        // ---------------------------------------------------------------------
        var records = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                { "Name", "John Doe" },
                { "Date", DateTime.Today.ToShortDateString() },
                { "Address", "123 Main St" }
            },
            new Dictionary<string, string>
            {
                { "Name", "Jane Smith" },
                { "Date", DateTime.Today.AddDays(-1).ToShortDateString() },
                { "Address", "456 Oak Ave" }
            }
        };

        // Iterate over each record and create a filled PDF
        for (int i = 0; i < records.Count; i++)
        {
            var fieldValues = records[i];

            // Bind the PDF template to a Form facade
            using (Form pdfForm = new Form(templatePdf))
            {
                // Fill each field with the corresponding value
                foreach (var kvp in fieldValues)
                {
                    // Skip null or empty values – Aspose.Pdf will ignore them
                    if (string.IsNullOrEmpty(kvp.Value))
                        continue;

                    pdfForm.FillField(kvp.Key, kvp.Value);
                }

                // Save the filled PDF; each record creates a separate file
                string outputPath = Path.Combine(outputDir, $"filled_{i + 1}.pdf");
                pdfForm.Save(outputPath);
            }
        }

        Console.WriteLine("All records have been exported to PDF form files.");
    }
}
