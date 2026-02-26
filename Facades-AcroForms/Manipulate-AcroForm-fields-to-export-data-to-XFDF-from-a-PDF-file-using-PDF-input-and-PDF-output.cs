using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the PDF document
        using (Form form = new Form(inputPdf))
        {
            // Export the AcroForm field data to an XFDF file
            using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF exported to '{outputXfdf}'.");
    }
}