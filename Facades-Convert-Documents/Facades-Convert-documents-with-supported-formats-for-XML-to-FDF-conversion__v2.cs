using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string fdfPath = "output.fdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Open the XML source stream and the destination FDF stream.
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        using (FileStream fdfStream = File.Create(fdfPath))
        {
            // Perform the conversion using the static FormDataConverter method.
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        Console.WriteLine($"Conversion completed. FDF saved to '{fdfPath}'.");
    }
}