using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string fdfOutputPath = "output.fdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Bind the Form facade to the source PDF
        using (Form form = new Form(sourcePdfPath))
        {
            // Export the form fields to an FDF file
            using (FileStream fdfStream = new FileStream(fdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
        }

        Console.WriteLine($"Form data successfully exported to '{fdfOutputPath}'.");
    }
}