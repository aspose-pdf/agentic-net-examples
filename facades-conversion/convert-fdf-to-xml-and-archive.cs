using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";
        const string archiveFolder = "Archive";

        // Verify the FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Ensure the archive folder exists
        if (!Directory.Exists(archiveFolder))
            Directory.CreateDirectory(archiveFolder);

        // Convert the FDF file to XML using the static method
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
        }

        // Verify the XML file was created
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Conversion failed, XML not created: {xmlPath}");
            return;
        }

        // Build destination path inside the archive folder
        string destPath = Path.Combine(archiveFolder, Path.GetFileName(xmlPath));

        // Move the XML file to the archive folder (overwrite if exists)
        if (File.Exists(destPath))
            File.Delete(destPath);
        File.Move(xmlPath, destPath);

        Console.WriteLine($"FDF converted to XML and moved to archive: {destPath}");
    }
}