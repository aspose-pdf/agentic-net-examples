using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFdfPath = "input.fdf";
        const string outputXmlPath = "output.xml";

        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {inputFdfPath}");
            return;
        }

        try
        {
            // Open source FDF stream for reading and destination XML stream for writing
            using (FileStream sourceStream = new FileStream(inputFdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                // Convert FDF to XML using the static FormDataConverter method
                FormDataConverter.ConvertFdfToXml(sourceStream, destStream);
            }

            Console.WriteLine($"Conversion completed. XML saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}