using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Filled.pdf";
        const string fieldName = "Text1";
        const string fieldValue = "sample text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Form form = new Form())
            {
                // Bind the existing PDF document
                form.BindPdf(inputPath);

                // Fill the textbox field
                form.FillField(fieldName, fieldValue);

                // Save the filled PDF
                form.Save(outputPath);
            }

            Console.WriteLine($"Form field '{fieldName}' filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}