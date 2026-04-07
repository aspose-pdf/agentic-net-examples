using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF into the Form facade
        using (Form form = new Form(inputPath))
        {
            // ---- Fill form fields (replace with actual field names/values) ----
            // Text field example
            form.FillField("FirstName", "John");
            form.FillField("LastName",  "Doe");

            // Checkbox field example
            form.FillField("AgreeTerms", true);

            // Radio button / list box / combo box can be filled similarly:
            // form.FillField("Gender", 1);          // selects the first option
            // form.FillField("Country", "USA");    // selects a value by name
            // ------------------------------------------------------------------

            // Flatten all fields so they become static content (non‑editable)
            form.FlattenAllFields();

            // Save the resulting PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}