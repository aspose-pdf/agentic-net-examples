using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the input PDF form, the JSON data file and the output PDF.
        const string inputPdfPath  = "template_form.pdf";
        const string jsonDataPath  = "field_values.json";
        const string outputPdfPath = "filled_form.pdf";

        // Ensure the input files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Open the PDF form with the Facades Form class.
        using (Form form = new Form(inputPdfPath))
        {
            // Load the JSON stream and import the field values.
            using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
            {
                // ImportJson matches fields by their full names.
                form.ImportJson(jsonStream);
            }

            // Save the updated PDF to the desired output file.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields filled and saved to '{outputPdfPath}'.");
    }
}