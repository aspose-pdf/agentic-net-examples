using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string fdfPath = "input.fdf";          // Path to the source FDF file
        const string xmlPath = "output.xml";         // Path where the resulting XML will be saved

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Source FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Open the source FDF stream for reading
            using (FileStream srcStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            // Create the destination XML stream for writing
            using (FileStream destStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Convert the FDF data to XML, preserving all form field data and structure
                FormDataConverter.ConvertFdfToXml(srcStream, destStream);
            }

            Console.WriteLine($"FDF successfully converted to XML: {xmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}