using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "filled_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the Form facade to fill form fields. This preserves the original layout.
        using (Form form = new Form())
        {
            // Load the existing PDF document
            form.BindPdf(inputPath);

            // Fill the required fields
            form.FillField("Name", "John Doe");
            form.FillField("Date", DateTime.Now.ToShortDateString());

            // Save the modified PDF to the specified output path
            form.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
