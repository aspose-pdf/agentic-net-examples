using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Root folder containing FDF files (may have nested subfolders)
        const string inputRoot = @"C:\InputFdf";
        // Root folder where the XML files will be written, mirroring the input hierarchy
        const string outputRoot = @"C:\OutputXml";

        if (!Directory.Exists(inputRoot))
        {
            Console.Error.WriteLine($"Input directory not found: {inputRoot}");
            return;
        }

        // Find all .fdf files recursively
        string[] fdfFiles = Directory.GetFiles(inputRoot, "*.fdf", SearchOption.AllDirectories);

        foreach (string fdfPath in fdfFiles)
        {
            // Compute the path relative to the input root
            string relativePath = Path.GetRelativePath(inputRoot, fdfPath);
            // Replace the .fdf extension with .xml
            string xmlRelative = Path.ChangeExtension(relativePath, ".xml");
            // Build the full output path under the output root
            string xmlPath = Path.Combine(outputRoot, xmlRelative);

            // Ensure the target directory exists
            string xmlDir = Path.GetDirectoryName(xmlPath);
            if (!string.IsNullOrEmpty(xmlDir) && !Directory.Exists(xmlDir))
                Directory.CreateDirectory(xmlDir);

            try
            {
                // Convert using streams – ConvertFdfToXml is a static method
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
                }

                Console.WriteLine($"Converted: {fdfPath} -> {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert {fdfPath}: {ex.Message}");
            }
        }
    }
}
