using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the FDF file; spaces are handled correctly by using the string directly
        const string fdfPath = "input file with spaces.fdf";
        const string xmlPath = "output.xml";

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Open the FDF file for reading and the XML file for writing.
        // Using blocks ensure streams are closed/disposed even if an exception occurs.
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            // ConvertFdfToXml is a static method; call it via the type name.
            FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
        }

        Console.WriteLine($"Conversion completed. XML saved to '{xmlPath}'.");
    }
}
