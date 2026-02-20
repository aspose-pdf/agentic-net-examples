using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputXmlPath> <outputFdfPath>
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <inputXmlPath> <outputFdfPath>");
            return;
        }

        string xmlPath = args[0];
        string fdfPath = args[1];

        // Validate input XML file
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Open input XML stream and output FDF stream
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                // Convert XML data to FDF using Aspose.Pdf.Facades.FormDataConverter
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
            }

            Console.WriteLine($"Successfully converted XML to FDF. Output saved at '{fdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}