using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source FDF file and the destination XML file
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";

        // Verify that the source FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Source FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Open the source FDF stream for reading
            using (FileStream sourceFdf = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            // Create the destination XML stream for writing
            using (FileStream destXml = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the FDF data to XML using the static FormDataConverter method
                FormDataConverter.ConvertFdfToXml(sourceFdf, destXml);
            }

            Console.WriteLine($"FDF successfully converted to XML: {xmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}