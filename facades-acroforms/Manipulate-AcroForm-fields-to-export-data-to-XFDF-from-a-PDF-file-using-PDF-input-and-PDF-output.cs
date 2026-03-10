using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF to the Form facade and export its fields to XFDF
        using (Form form = new Form())
        {
            form.BindPdf(inputPdfPath); // Load the PDF document

            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream); // Write XFDF data to the stream
            }
        }

        Console.WriteLine($"XFDF exported successfully to '{outputXfdfPath}'.");
    }
}