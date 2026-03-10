using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XML file and the destination FDF file
        const string xmlFilePath = "input.xml";
        const string fdfFilePath = "output.fdf";

        // Validate input file existence
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }

        // Convert XML form data to FDF using the static FormDataConverter method
        // Both streams are wrapped in using blocks to ensure proper disposal
        using (FileStream xmlStream = File.OpenRead(xmlFilePath))
        using (FileStream fdfStream = File.Create(fdfFilePath))
        {
            // The ConvertXmlToFdf method reads the XML from xmlStream
            // and writes the resulting FDF content to fdfStream
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"FDF file created at: {fdfFilePath}");
    }
}