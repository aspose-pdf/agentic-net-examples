using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string fdfPath = "input.fdf";
        const string xmlOutputDir = "Output";

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(xmlOutputDir);

        // Determine creation date from the FDF file (fallback to file system creation time)
        DateTime creationDate = File.GetCreationTime(fdfPath);

        // Convert FDF to XML using FormDataConverter (static method, no instance needed)
        string tempXmlPath = Path.Combine(xmlOutputDir, "temp.xml");
        using (FileStream fdfStream = File.OpenRead(fdfPath))
        using (FileStream xmlStream = File.Create(tempXmlPath))
        {
            FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
        }

        // Build new filename that includes the creation date (yyyyMMdd_HHmmss)
        string datePart = creationDate.ToString("yyyyMMdd_HHmmss");
        string baseName = Path.GetFileNameWithoutExtension(fdfPath);
        string newFileName = $"{baseName}_{datePart}.xml";
        string finalXmlPath = Path.Combine(xmlOutputDir, newFileName);

        // Rename the temporary XML file to the final name (overwrite if exists)
        if (File.Exists(finalXmlPath))
            File.Delete(finalXmlPath);

        File.Move(tempXmlPath, finalXmlPath);

        Console.WriteLine($"FDF converted to XML: {finalXmlPath}");
    }
}
