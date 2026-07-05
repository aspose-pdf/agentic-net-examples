using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pcl";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PCL load options.
        PclLoadOptions loadOptions = new PclLoadOptions();

        // If the library version supports it, enable HP‑GL/2 vector loading.
        // Uncomment the following line when the property is available:
        // loadOptions.EnableHPGL2 = true;

        // Load the PCL file with the specified options and save as PDF.
        using (Document pdfDocument = new Document(inputPath, loadOptions))
        {
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PCL file has been converted to PDF: {outputPath}");
    }
}