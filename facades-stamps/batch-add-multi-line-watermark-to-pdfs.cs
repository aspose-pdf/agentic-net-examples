using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchWatermark
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // NOTE: The original project produced NU1902/NU1903 warnings because it
        // referenced System.Security.Cryptography.Pkcs 9.0.0, which has known
        // vulnerabilities.  To eliminate the warnings (and treat them as errors
        // if the build is configured with "Treat warnings as errors"), update the
        // package reference in the *.csproj file to a newer, patched version, e.g.:
        //
        //   <PackageReference Update="System.Security.Cryptography.Pkcs" Version="9.0.1" />
        //
        // Alternatively, you can suppress the specific warnings with:
        //   <NoWarn>NU1902;NU1903</NoWarn>
        // ---------------------------------------------------------------------

        // Resolve base directory (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input and output folders (relative to the executable location)
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "WatermarkedPdfs");

        // Validate input folder – if it does not exist, inform the user and exit.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"[Error] Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists.
        Directory.CreateDirectory(outputFolder);

        // Multi‑line watermark text
        string watermarkText = "Confidential\nDo Not Distribute";

        // Create a reusable TextStamp (the same instance can be added to many pages)
        TextStamp stamp = new TextStamp(watermarkText)
        {
            TextState = {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 48,
                ForegroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.8, 0.8)
            },
            Opacity = 0.3,
            RotateAngle = 45,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputFolder, fileName);

            // Verify the source file exists (defensive programming – GetFiles already filters, but keep for safety)
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"[Warning] Skipping missing file: {pdfPath}");
                continue;
            }

            try
            {
                // Load source PDF
                using (Document srcDoc = new Document(pdfPath))
                {
                    // Apply the watermark to every page
                    foreach (Page page in srcDoc.Pages)
                    {
                        page.AddStamp(stamp);
                    }

                    // Save the watermarked document
                    srcDoc.Save(outPath);
                }

                Console.WriteLine($"Watermarked: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Error] Failed to process '{fileName}': {ex.Message}");
            }
        }
    }
}
