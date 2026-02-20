using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input FDF file and output XML file paths
        string fdfPath = "input.fdf";
        string xmlPath = "output.xml";

        // Verify that the source FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found at '{fdfPath}'.");
            return;
        }

        try
        {
            // Open the FDF file for reading and create the XML file for writing
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            using (FileStream xmlStream = File.Create(xmlPath))
            {
                // Convert FDF to XML using the Facades API
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"FDF file successfully converted to XML: '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}