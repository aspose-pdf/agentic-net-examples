using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder that contains the FDF files (relative to the executable directory)
        string fdfFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FdfFiles");

        if (!Directory.Exists(fdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {fdfFolder}");
            return;
        }

        // Get all *.fdf files in the folder
        string[] fdfFiles = Directory.GetFiles(fdfFolder, "*.fdf", SearchOption.TopDirectoryOnly);
        if (fdfFiles.Length == 0)
        {
            Console.WriteLine("No FDF files found to process.");
            return;
        }

        // FormDataConverter members are static – no need to instantiate
        foreach (string fdfPath in fdfFiles)
        {
            try
            {
                // Destination XML file name (same name, .xml extension)
                string xmlPath = Path.ChangeExtension(fdfPath, ".xml");

                // Use the stream‑based overloads of ConvertFdfToXml
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
                }

                Console.WriteLine($"Converted: '{Path.GetFileName(fdfPath)}' → '{Path.GetFileName(xmlPath)}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(fdfPath)}': {ex.Message}");
            }
        }
    }
}
