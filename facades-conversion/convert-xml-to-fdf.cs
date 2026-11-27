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

        // Open the XML file as a read‑only stream.
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        // Destination stream that will receive the FDF data.
        using (MemoryStream fdfStream = new MemoryStream())
        {
            // Convert the XML stream to an FDF stream using the static method.
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);

            // Reset the position so we can read from the beginning.
            fdfStream.Position = 0;

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(fdfPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Write the FDF stream to a file.
            using (FileStream file = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                fdfStream.CopyTo(file);
            }
        }

        Console.WriteLine($"FDF file saved to '{fdfPath}'.");
    }
}
