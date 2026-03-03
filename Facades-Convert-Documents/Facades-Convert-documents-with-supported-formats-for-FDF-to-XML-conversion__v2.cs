using System;
using System.IO;
using Aspose.Pdf.Facades; // Provides FormDataConverter for FDF ↔ XML conversion

class Program
{
    static void Main()
    {
        // Paths to source FDF file and destination XML file
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";

        // Validate input file existence
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Open source FDF stream for reading and destination XML stream for writing
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Perform conversion using the Facade class
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"FDF successfully converted to XML: {xmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}