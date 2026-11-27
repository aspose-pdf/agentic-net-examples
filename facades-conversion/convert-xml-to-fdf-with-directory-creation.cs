using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputDir = "output";
        // Path.Combine is evaluated at runtime, so the variable cannot be const.
        string fdfPath = Path.Combine(outputDir, "output.fdf");

        // Verify the source XML exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create the destination directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Convert XML to FDF using the static method on FormDataConverter.
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (FileStream fdfStream = File.Create(fdfPath))
        {
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"FDF file successfully saved to '{fdfPath}'.");
    }
}