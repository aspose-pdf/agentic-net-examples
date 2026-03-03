using System;
using System.IO;
using Aspose.Pdf.Facades; // Provides FormDataConverter

class Program
{
    static void Main()
    {
        // Paths for input FDF and output XML files
        const string inputFdfPath  = "input.fdf";
        const string outputXmlPath = "output.xml";

        // Verify the source FDF file exists
        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"Source FDF file not found: {inputFdfPath}");
            return;
        }

        try
        {
            // Create streams for source FDF (read) and destination XML (write)
            using (FileStream sourceStream = new FileStream(inputFdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream   = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                // Convert FDF to XML using the static FormDataConverter method
                FormDataConverter.ConvertFdfToXml(sourceStream, destStream);
            }

            Console.WriteLine($"FDF successfully converted to XML: {outputXmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}