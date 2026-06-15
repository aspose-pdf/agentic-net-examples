using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "data.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Initialize the Form facade with source and destination PDFs
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the XFDF file as a stream
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import form field values from the XFDF stream
                form.ImportXfdf(xfdfStream);
            }

            // Save the updated PDF with imported field values
            form.Save();
        }

        Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdfPath}'.");
    }
}