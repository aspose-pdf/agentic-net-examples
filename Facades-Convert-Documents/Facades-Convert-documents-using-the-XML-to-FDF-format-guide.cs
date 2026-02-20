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
            Console.Error.WriteLine("Usage: <program> <inputXmlPath> <outputFdfPath>");
            return;
        }

        string xmlPath = args[0];
        string fdfPath = args[1];

        // Verify that the input XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Open the XML file for reading and create the FDF file for writing
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            using (FileStream fdfStream = File.Create(fdfPath))
            {
                // Instantiate the converter (constructor provided by the API)
                FormDataConverter converter = new FormDataConverter();

                // Perform the conversion from XML to FDF
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Successfully converted XML to FDF. Output saved at '{fdfPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during conversion
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}