using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPattern = "page_{0}.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Substitute any missing font with Arial
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

            // Create a JPEG device (resolution 300 DPI, quality 90)
            // JpegDevice does NOT implement IDisposable, so do NOT use a using statement.
            // Quality is supplied via the constructor, not via a property.
            JpegDevice jpegDevice = new JpegDevice(new Resolution(300), 90);

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                string outputPath = string.Format(outputPattern, i);

                // Render the page to a JPEG file using a FileStream
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(page, outStream);
                }

                Console.WriteLine($"Saved page {i} as {outputPath}");
            }
        }
    }
}
