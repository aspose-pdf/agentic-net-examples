using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName  = "TextBox1";          // name of the textbox field in the PDF
        const string fieldValue = "Hello, World!";    // value to set

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block
            using (Form form = new Form())
            {
                // Load the existing PDF document
                form.BindPdf(inputPath);

                // Fill the specified textbox field with the desired value
                form.FillField(fieldName, fieldValue);

                // Save the updated PDF to a new file
                form.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}