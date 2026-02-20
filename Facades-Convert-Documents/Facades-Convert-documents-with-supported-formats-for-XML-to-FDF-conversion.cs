using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input XML file path and output FDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputXmlPath> <outputFdfPath>");
            return;
        }

        string xmlPath = args[0];
        string fdfPath = args[1];

        // Verify that the input XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Open the XML file for reading and create the FDF file for writing
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            using (FileStream fdfStream = File.Create(fdfPath))
            {
                // Perform the conversion from XML to FDF
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Conversion successful. FDF saved to: {fdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}