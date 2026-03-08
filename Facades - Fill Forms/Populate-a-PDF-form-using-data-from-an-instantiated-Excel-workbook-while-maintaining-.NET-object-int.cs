using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Simulated Excel workbook data (field name -> value)
        var fieldValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToString("yyyy-MM-dd") },
            { "Address", "123 Main St" }
        };

        try
        {
            // Load the PDF form using the Form facade
            using (Form pdfForm = new Form(inputPdf))
            {
                // Fill each field with the corresponding value
                foreach (var kvp in fieldValues)
                {
                    bool success = pdfForm.FillField(kvp.Key, kvp.Value);
                    if (!success)
                    {
                        Console.WriteLine($"Warning: Field '{kvp.Key}' not found or could not be filled.");
                    }
                }

                // Save the updated PDF
                pdfForm.Save(outputPdf);
            }

            Console.WriteLine($"PDF form populated and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}