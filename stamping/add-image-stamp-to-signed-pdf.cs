using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";               // digitally signed PDF
        const string stampPath  = "stamp.png";                // image to stamp
        const string outputPath = "signed_with_stamp.pdf";    // result preserving signature

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        try
        {
            // Copy the original signed PDF to the output location first.
            // This allows us to open the output file in read/write mode and perform an incremental update.
            File.Copy(inputPath, outputPath, true);

            // Open the copied PDF with a read/write FileStream so that Save() performs an incremental update.
            using (FileStream fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
            using (Document doc = new Document(fs))
            {
                // Create an image stamp; set it as an overlay with semi‑transparent opacity.
                ImageStamp imgStamp = new ImageStamp(stampPath)
                {
                    Background          = false,
                    Opacity             = 0.5,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Apply the stamp to every page.
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Saving without explicit options performs an incremental update because the document
                // was opened on a read/write stream.
                doc.Save();
            }

            Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
