using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFdf = "output.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the Form facade and export its fields to an FDF file
        using (Form form = new Form(inputPdf))
        {
            using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"FDF exported to '{outputFdf}'.");
    }
}