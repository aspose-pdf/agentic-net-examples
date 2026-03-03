using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source FDF file and the destination XML file.
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";

        // Verify that the source file exists.
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found at '{fdfPath}'.");
            return;
        }

        // Open the source and destination streams within using blocks to ensure proper disposal.
        using (FileStream sourceStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream destinationStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            // Perform the conversion from FDF to XML using the static method provided by Aspose.Pdf.Facades.
            FormDataConverter.ConvertFdfToXml(sourceStream, destinationStream);
        }

        Console.WriteLine($"Conversion completed. XML saved to '{xmlPath}'.");
    }
}