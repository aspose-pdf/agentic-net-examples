using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XML data file and the destination FDF file
        const string xmlPath = "data.xml";
        const string fdfPath = "output.fdf";

        // Verify that the XML file exists before proceeding
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Open the XML file for reading and the FDF file for writing.
        // The using statements ensure proper disposal of the streams.
        using (FileStream srcXml = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream destFdf = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            // Convert the XML form data to FDF format.
            // This static method preserves the field mappings defined in the XML.
            FormDataConverter.ConvertXmlToFdf(srcXml, destFdf);
        }

        Console.WriteLine($"FDF file successfully created at '{fdfPath}'.");
    }
}