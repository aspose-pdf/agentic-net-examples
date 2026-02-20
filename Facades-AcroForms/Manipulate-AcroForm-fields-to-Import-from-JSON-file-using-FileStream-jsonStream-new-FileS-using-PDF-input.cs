using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, JSON data path, output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPdf> <jsonData> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string jsonDataPath = args[1];
        string outputPdfPath = args[2];

        // Validate file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(jsonDataPath))
        {
            Console.WriteLine($"Error: JSON data file not found at '{jsonDataPath}'.");
            return;
        }

        // Use the Form facade to bind the PDF and import JSON data
        using (Form form = new Form())
        {
            // Bind the existing PDF document
            form.BindPdf(inputPdfPath);

            // Import field values from the JSON stream
            using (FileStream jsonStream = File.OpenRead(jsonDataPath))
            {
                form.ImportJson(jsonStream);
            }

            // Save the modified PDF to the specified output path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Successfully imported JSON data into form fields and saved to '{outputPdfPath}'.");
    }
}