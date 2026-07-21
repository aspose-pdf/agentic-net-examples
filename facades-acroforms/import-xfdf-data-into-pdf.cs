using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize the Form facade with source and destination PDF files
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            // Open the XFDF file as a stream and import its field values
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"Form fields imported from '{xfdfPath}' and saved to '{outputPdfPath}'.");
    }
}