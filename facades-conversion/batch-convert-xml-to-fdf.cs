using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Directory containing the source XML files
        const string inputDirectory = "XmlInputs";
        // Directory where the generated FDF files will be placed
        const string outputDirectory = "FdfOutputs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each XML file in the input directory
        foreach (string xmlFilePath in Directory.GetFiles(inputDirectory, "*.xml"))
        {
            string baseName = Path.GetFileNameWithoutExtension(xmlFilePath);
            string fdfFilePath = Path.Combine(outputDirectory, baseName + ".fdf");

            try
            {
                // Open the XML source and the target FDF stream
                using (FileStream xmlStream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream fdfStream = new FileStream(fdfFilePath, FileMode.Create, FileAccess.Write))
                {
                    // Convert XML to FDF using the static method
                    Aspose.Pdf.Facades.FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
                }

                Console.WriteLine($"Converted: {xmlFilePath} → {fdfFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert '{xmlFilePath}': {ex.Message}");
            }
        }
    }
}