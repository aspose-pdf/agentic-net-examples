using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string fdfPath = "input.fdf";

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Determine the XML output filename and append "_converted" before the extension
        string xmlDirectory = Path.GetDirectoryName(fdfPath) ?? string.Empty;
        string xmlBaseName  = Path.GetFileNameWithoutExtension(fdfPath);
        string convertedXmlPath = Path.Combine(xmlDirectory, $"{xmlBaseName}_converted.xml");

        try
        {
            // FormDataConverter provides static conversion methods. Use ConvertFdfToXml for FDF → XML.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            using (FileStream xmlStream = File.Create(convertedXmlPath))
            {
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"Converted XML saved to '{convertedXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
