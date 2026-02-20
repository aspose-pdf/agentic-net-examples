using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input FDF file path and output XML file path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <input.fdf> <output.xml>");
            return;
        }

        string fdfPath = args[0];
        string xmlPath = args[1];

        // Verify that the input FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found at '{fdfPath}'.");
            return;
        }

        try
        {
            // Open streams for reading the FDF and writing the XML
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                // Convert FDF to XML using Aspose.Pdf.Facades.FormDataConverter
                FormDataConverter.ConvertFdfToXml(fdfStream, xmlStream);
            }

            Console.WriteLine($"FDF file successfully converted to XML and saved at '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}