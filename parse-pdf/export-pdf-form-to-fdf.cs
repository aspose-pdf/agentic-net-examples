using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Use the Form class from Aspose.Pdf.Facades via fully qualified name (no using directive for that namespace)
        Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(inputPdfPath);

        // Export form fields to an FDF file and ensure the stream is closed
        using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
        {
            form.ExportFdf(fdfStream);
        }

        // Close the Form facade
        form.Close();

        Console.WriteLine($"Form data exported to FDF: {outputFdfPath}");
    }
}