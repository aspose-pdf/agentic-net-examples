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

        // Load the PCL file with default options (SuppressErrors is false by default)
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the loaded document as PDF (default Save writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PCL converted to PDF and saved as '{outputPath}'.");
    }
}