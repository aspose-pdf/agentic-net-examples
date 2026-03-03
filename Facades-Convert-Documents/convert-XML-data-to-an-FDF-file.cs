using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string fdfPath = "output.fdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Open source XML and destination FDF streams with deterministic disposal
        using (FileStream srcXml = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        using (FileStream destFdf = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            // Convert XML to FDF using Aspose.Pdf.Facades.FormDataConverter
            FormDataConverter.ConvertXmlToFdf(srcXml, destFdf);
        }

        Console.WriteLine($"XML data successfully converted to FDF: {fdfPath}");
    }
}