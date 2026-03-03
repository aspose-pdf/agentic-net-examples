using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XML file and the destination FDF file.
        const string inputXmlPath = "input.xml";
        const string outputFdfPath = "output.fdf";

        // Verify that the source XML file exists.
        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Source XML file not found: {inputXmlPath}");
            return;
        }

        try
        {
            // Open the XML file for reading and the FDF file for writing.
            using (FileStream xmlStream = File.OpenRead(inputXmlPath))
            using (FileStream fdfStream = File.Create(outputFdfPath))
            {
                // Convert the XML form data to FDF format using the Facades API.
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"XML successfully converted to FDF: {outputFdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}