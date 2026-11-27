using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Convert the XML file to an FDF file using FormDataConverter.
        // ConvertXmlToFdf is a static method, so call it on the type directly.
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        // Retrieve the creation date of the original XML file.
        DateTime creationDate = File.GetCreationTime(xmlPath);
        string timestamp = creationDate.ToString("yyyyMMdd_HHmmss");

        // Build a new filename that includes the creation timestamp.
        string directory = Path.GetDirectoryName(fdfPath) ?? string.Empty;
        string newFileName = $"output_{timestamp}.fdf";
        string newFilePath = Path.Combine(directory, newFileName);

        // Rename (move) the generated FDF file to the new name.
        try
        {
            File.Move(fdfPath, newFilePath);
            Console.WriteLine($"FDF file renamed to: {newFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error renaming FDF file: {ex.Message}");
        }
    }
}
