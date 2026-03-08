using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF form template and the output file.
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_form.pdf";

        // Verify that the template exists.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Sample data to populate the form.
        // Keys must match the full field names defined in the PDF.
        var fieldValues = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName",  "Doe" },
            { "Date",      DateTime.Today.ToShortDateString() },
            { "Address",   "123 Main St, Anytown" }
        };

        try
        {
            // Bind the Form facade to the template PDF.
            using (Form pdfForm = new Form(templatePath))
            {
                // Fill each field with the corresponding value.
                foreach (var kvp in fieldValues)
                {
                    bool success = pdfForm.FillField(kvp.Key, kvp.Value);
                    if (!success)
                    {
                        Console.WriteLine($"Warning: Field '{kvp.Key}' not found or could not be filled.");
                    }
                }

                // Save the filled PDF to the specified output path.
                pdfForm.Save(outputPath);
            }

            Console.WriteLine($"PDF form successfully filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during form filling: {ex.Message}");
        }
    }
}