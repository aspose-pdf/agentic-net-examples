using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF with a textbox field
        const string outputPath = "filled.pdf";  // destination PDF after filling
        const string fieldName  = "TextBox1";    // exact field name (case‑sensitive)
        const string fieldValue = "Hello World"; // value to set

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the Form facade on the source PDF
            using (Form form = new Form(inputPath))
            {
                // Fill the specified textbox field
                bool success = form.FillField(fieldName, fieldValue);
                if (!success)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or could not be filled.");
                }

                // Save the updated PDF to the output path
                form.Save(outputPath);
            }

            Console.WriteLine($"PDF with filled field saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}