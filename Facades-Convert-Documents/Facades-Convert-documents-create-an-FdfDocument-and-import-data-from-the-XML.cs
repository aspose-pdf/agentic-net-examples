using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input XML file containing form data
        const string xmlPath = "data.xml";
        // Output FDF file that will be created from the XML
        const string fdfPath = "output.fdf";

        // Verify that the XML source file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Open the XML file for reading
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            // Create a memory stream to hold the generated FDF data
            using (MemoryStream fdfStream = new MemoryStream())
            {
                // Convert XML to FDF using the Facade API
                FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);

                // Reset the position of the memory stream before saving to disk
                fdfStream.Position = 0;

                // Write the FDF content to the output file
                using (FileStream output = File.Create(fdfPath))
                {
                    fdfStream.CopyTo(output);
                }

                Console.WriteLine($"FDF file successfully created at '{fdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}