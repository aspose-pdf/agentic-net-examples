using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Determine the input folder relative to the executable location.
        // If it does not exist, create it so the sample can run out‑of‑the‑box.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputDirectory = Path.Combine(baseDir, "Input");
        if (!Directory.Exists(inputDirectory))
        {
            Directory.CreateDirectory(inputDirectory);
            Console.WriteLine($"Input directory not found. Created at: {inputDirectory}");
        }

        // Retrieve all FDF files in the input folder.
        string[] fdfFiles = Directory.GetFiles(inputDirectory, "*.fdf");
        if (fdfFiles.Length == 0)
        {
            Console.WriteLine($"No FDF files found in '{inputDirectory}'.");
            return;
        }

        foreach (string fdfPath in fdfFiles)
        {
            // Preserve the original file name (including the date stamp) and change only the extension to .xml
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fdfPath);
            string xmlPath = Path.Combine(Path.GetDirectoryName(fdfPath) ?? inputDirectory,
                                          fileNameWithoutExt + ".xml");

            try
            {
                // Convert the FDF file to XML using Aspose.Pdf.Facades.FormDataConverter
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                using (FileStream xmlStream = File.Create(xmlPath))
                {
                    FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
                }

                Console.WriteLine($"Converted '{fdfPath}' to '{xmlPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert '{fdfPath}': {ex.Message}");
            }
        }
    }
}
