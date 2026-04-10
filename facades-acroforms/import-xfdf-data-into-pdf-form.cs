using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath      = "data.xfdf";

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

        // Initialize the Form facade with source and destination PDF files
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the XFDF file stream and import field values into the PDF
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Save the PDF with the imported form data
            form.Save();
        }

        Console.WriteLine($"Form fields imported from '{xfdfPath}' and saved to '{outputPdfPath}'.");
    }
}