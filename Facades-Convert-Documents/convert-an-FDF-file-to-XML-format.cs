using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string fdfPath = "input.fdf";
        const string xmlPath = "output.xml";

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Source FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Create source and destination streams (lifecycle: create, load, save)
            using (FileStream srcStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream destStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Convert FDF to XML using the provided API
                FormDataConverter.ConvertFdfToXml(srcStream, destStream);
            }

            Console.WriteLine($"FDF successfully converted to XML: {xmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}