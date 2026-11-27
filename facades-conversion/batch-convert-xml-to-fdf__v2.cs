using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the folder containing XML files (relative to the executable)
        string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XmlInputs");

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Get all XML files in the folder (non‑recursive)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml", SearchOption.TopDirectoryOnly);

        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to convert.");
            return;
        }

        // No need to instantiate FormDataConverter – its ConvertXmlToFdf method is static
        foreach (string xmlPath in xmlFiles)
        {
            // Build the output FDF file path (same name, .fdf extension)
            string fdfPath = Path.ChangeExtension(xmlPath, ".fdf");

            try
            {
                // Open the source XML and destination FDF as streams; streams are disposable
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the XML form data to FDF format using the static overload
                    FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
                }

                Console.WriteLine($"Converted: {Path.GetFileName(xmlPath)} → {Path.GetFileName(fdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
