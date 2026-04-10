using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "data.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Initialize Form facade with source PDF and target output file
        Form form = new Form(pdfPath, outputPath);

        // Import XFDF field values
        using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
        {
            form.ImportXfdf(xfdfStream);
        }

        // Save the updated PDF
        form.Save();

        // Release resources
        form.Close();

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}