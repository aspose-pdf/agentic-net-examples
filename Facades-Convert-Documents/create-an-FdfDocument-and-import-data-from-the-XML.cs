using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        // Verify that the source XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        // Open the XML file for reading and create a stream for the resulting FDF
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            // Convert the XML data to FDF using Aspose.Pdf.Facades.FormDataConverter
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"FDF document successfully created at '{fdfPath}'.");
    }
}