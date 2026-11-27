using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source XML, generated FDF and archive folder
        const string xmlPath = "data.xml";
        const string fdfPath = "output.fdf";
        const string archiveFolder = "Archive";

        // Verify that the source XML file exists before attempting conversion
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file not found at '{Path.GetFullPath(xmlPath)}'." );
            Console.WriteLine("Please ensure the file exists or provide a valid path.");
            return;
        }

        // Ensure the archive directory exists
        Directory.CreateDirectory(archiveFolder);

        // Convert the XML file to an FDF file using FormDataConverter
        // The method is static, so we call it directly on the type.
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        // Verify that the FDF file was actually created before moving it
        if (!File.Exists(fdfPath))
        {
            Console.WriteLine($"Error: Failed to create FDF file at '{Path.GetFullPath(fdfPath)}'.");
            return;
        }

        // Move the generated FDF file to the archive folder
        string destinationPath = Path.Combine(archiveFolder, Path.GetFileName(fdfPath));

        // If a file with the same name already exists in the archive, replace it
        if (File.Exists(destinationPath))
        {
            File.Delete(destinationPath);
        }

        File.Move(fdfPath, destinationPath);
        Console.WriteLine($"FDF file successfully archived to '{destinationPath}'.");
    }
}
