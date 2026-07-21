using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filled.pdf";
        const string fieldName = "TextBox1";
        const string fieldValue = "Hello World";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF into the Form facade
        using (Form form = new Form(inputPath))
        {
            // Fill the specified text box field
            bool success = form.FillField(fieldName, fieldValue);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to fill field '{fieldName}'.");
            }

            // Save the updated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}