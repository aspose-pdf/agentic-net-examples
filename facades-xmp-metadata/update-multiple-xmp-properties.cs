using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a temporary file to ensure atomic update
        string tempPath = Path.GetTempFileName();

        try
        {
            // Load the PDF and bind its XMP metadata
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPath);

                // Update multiple XMP properties in one transaction
                xmp.Add(DefaultMetadataProperties.Nickname, "MyNick");
                xmp.Add(DefaultMetadataProperties.CreatorTool, "MyApp");
                xmp.Add(DefaultMetadataProperties.ModifyDate, DateTime.UtcNow.ToString("o"));

                // Save the updated PDF (with new XMP) to a temporary file
                xmp.Save(tempPath);
            }

            // Replace the original file with the updated version
            File.Copy(tempPath, outputPath, true);
            Console.WriteLine($"XMP metadata updated successfully: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }
    }
}