using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source FDF files
        const string inputDirectory = "FdfFiles";
        // Directory where the resulting XML files will be placed
        const string outputDirectory = "XmlOutput";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Retrieve all .fdf files in the input directory (non‑recursive)
        string[] fdfFiles = Directory.GetFiles(inputDirectory, "*.fdf", SearchOption.TopDirectoryOnly);

        foreach (string fdfPath in fdfFiles)
        {
            // Build the output XML path with the same base filename
            string baseName = Path.GetFileNameWithoutExtension(fdfPath);
            string xmlPath = Path.Combine(outputDirectory, baseName + ".xml");

            // ConvertFdfToXml expects streams, so open the files accordingly.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // ConvertFdfToXml is a static method – call it via the type name.
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"Converted '{fdfPath}' to '{xmlPath}'.");
        }
    }
}
