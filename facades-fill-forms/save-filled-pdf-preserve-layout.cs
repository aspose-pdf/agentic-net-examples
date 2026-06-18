using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (already filled or to be filled)
        const string inputPath  = "input.pdf";
        // Desired output path – original layout will be kept
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the Form facade and bind the PDF
        Form form = new Form();
        form.BindPdf(inputPath);

        // OPTIONAL: fill form fields (example – fill every field with a placeholder value)
        foreach (string fieldName in form.FieldNames)
        {
            // Adjust as needed; here we simply set a generic text
            form.FillField(fieldName, "Sample value");
        }

        // Save the PDF using the facade's Save method.
        // This writes the document to the specified path while preserving the original layout.
        form.Save(outputPath);

        // Release resources held by the facade
        form.Close();

        Console.WriteLine($"PDF saved to '{outputPath}' with original layout preserved.");
    }
}