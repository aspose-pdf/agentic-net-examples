using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input XML file containing form data
        const string xmlPath = "formData.xml";
        // Destination FDF file
        const string fdfPath = "formData.fdf";

        // Validate input file existence
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML file not found: {xmlPath}");
            return;
        }

        // Open streams for source XML and destination FDF
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (FileStream fdfStream = File.Create(fdfPath))
        {
            // Convert XML form data to FDF using Aspose.Pdf.Facades
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"XML data successfully converted to FDF: {fdfPath}");
    }
}