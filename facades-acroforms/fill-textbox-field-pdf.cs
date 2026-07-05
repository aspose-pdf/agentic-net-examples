using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the form
        const string outputPath = "filled.pdf";  // destination PDF after filling
        const string fieldName  = "TextBox1";    // exact name of the textbox field (case‑sensitive)
        const string fieldValue = "Hello World"; // value to set in the textbox

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the input PDF
            Form form = new Form(inputPath);

            // Attempt to fill the specified textbox field
            bool success = form.FillField(fieldName, fieldValue);
            if (!success)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" was not found or could not be filled.");
            }

            // Save the updated PDF to the output path
            form.Save(outputPath);

            // Release resources held by the facade
            form.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine($"PDF saved to \"{outputPath}\".");
    }
}