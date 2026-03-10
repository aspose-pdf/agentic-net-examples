using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Output PDF after any modifications (if needed)
        const string outputPdfPath = "output.pdf";

        // Destination for the exported FDF data
        const string fdfOutputPath = "exported.fdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF as a Form facade
        using (Form form = new Form(inputPdfPath))
        {
            // OPTIONAL: manipulate form fields before exporting
            // Example: set a text field named "CustomerName" to "John Doe"
            // Use the fully qualified field name as required by the API
            // form.FillField("CustomerName", "John Doe");

            // Export the current field values to an FDF stream
            using (FileStream fdfStream = new FileStream(fdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
                // The stream will be closed automatically by the using block
            }

            // Save the (potentially modified) PDF to a new file
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"FDF exported to '{fdfOutputPath}'.");
        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}