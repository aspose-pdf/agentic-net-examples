using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing; // Required for parsing hex color to System.Drawing.Color

// Configuration model for watermark settings
public class WatermarkConfig
{
    // Provide default empty strings to satisfy non‑nullable warnings
    public string InputFolder { get; set; } = string.Empty;          // Folder containing PDFs to process
    public string OutputFolder { get; set; } = string.Empty;         // Folder where watermarked PDFs will be saved
    public string WatermarkText { get; set; } = string.Empty;        // Text to display as watermark
    public string FontName { get; set; } = "Helvetica";
    public int FontSize { get; set; } = 36;
    public string ColorHex { get; set; } = "#C0C0C0"; // Light gray in hex
    public float Opacity { get; set; } = 0.5f;        // 0 (transparent) to 1 (opaque)
    public bool IsBackground { get; set; } = true;   // Place watermark behind page content
    public float OriginX { get; set; } = 100;        // X coordinate of watermark origin
    public float OriginY { get; set; } = 400;        // Y coordinate of watermark origin
}

// Helper utilities
public static class WatermarkUtils
{
    // Convert hex color string to System.Drawing.Color
    public static System.Drawing.Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            return System.Drawing.Color.Black;

        // Remove leading '#'
        hex = hex.TrimStart('#');

        if (hex.Length == 6)
        {
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        // Fallback
        return System.Drawing.Color.Black;
    }
}

// Main processing class
public class WatermarkProcessor
{
    private readonly WatermarkConfig _config;

    public WatermarkProcessor(WatermarkConfig config)
    {
        _config = config;
    }

    public void ApplyWatermarkToAllPdfs()
    {
        // Ensure output folder exists
        Directory.CreateDirectory(_config.OutputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(_config.InputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(_config.OutputFolder, fileName);

            // Initialize PdfFileStamp facade (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();

            // Bind the source PDF
            fileStamp.BindPdf(inputPath);

            // Create a text stamp (watermark) using FormattedText
            System.Drawing.Color sysColor = WatermarkUtils.ParseHexColor(_config.ColorHex);
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                _config.WatermarkText,
                sysColor,
                _config.FontName,
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,
                _config.FontSize);

            // Configure the stamp (fully qualified Stamp type)
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);                     // Attach the formatted text
            stamp.SetOrigin(_config.OriginX, _config.OriginY);
            stamp.Opacity = _config.Opacity;
            stamp.IsBackground = _config.IsBackground;

            // Add the stamp to the PDF
            fileStamp.AddStamp(stamp);

            // Save the watermarked PDF
            fileStamp.Save(outputPath);

            // Close the facade (releases internal resources)
            fileStamp.Close();
        }
    }
}

// Entry point
class Program
{
    static void Main()
    {
        // Load configuration from JSON file (watermarkConfig.json)
        const string configPath = "watermarkConfig.json";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        WatermarkConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            // JsonSerializer.Deserialize may return null – guard against it
            config = JsonSerializer.Deserialize<WatermarkConfig>(json) ?? new WatermarkConfig();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Validate required paths (they are non‑null because of defaults, but still check for emptiness)
        if (string.IsNullOrWhiteSpace(config.InputFolder) || string.IsNullOrWhiteSpace(config.OutputFolder))
        {
            Console.Error.WriteLine("InputFolder and OutputFolder must be specified in the configuration.");
            return;
        }

        // Process all PDFs
        try
        {
            WatermarkProcessor processor = new WatermarkProcessor(config);
            processor.ApplyWatermarkToAllPdfs();
            Console.WriteLine("Watermarking completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
