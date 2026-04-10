using System;
using System.IO;
using System.Drawing; // for Color and ColorTranslator
using System.Text.Json; // for reading JSON configuration
using Aspose.Pdf.Facades; // PdfFileStamp, Stamp, FormattedText, EncodingType

class WatermarkBatchProcessor
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Load configuration from appsettings.json (must be placed beside exe)
        // ---------------------------------------------------------------------
        const string configFileName = "appsettings.json";
        if (!File.Exists(configFileName))
        {
            Console.WriteLine($"Configuration file '{configFileName}' not found. Using defaults.");
        }

        // Parse JSON configuration (if present) using System.Text.Json
        JsonElement root = default;
        if (File.Exists(configFileName))
        {
            try
            {
                string json = File.ReadAllText(configFileName);
                using JsonDocument doc = JsonDocument.Parse(json);
                root = doc.RootElement.Clone(); // clone to keep after using block
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read configuration file: {ex.Message}. Using defaults.");
            }
        }

        // Helper local function to safely retrieve a string value from the JSON tree
        string GetConfigString(string section, string key, string @default)
        {
            try
            {
                if (root.ValueKind != JsonValueKind.Undefined &&
                    root.TryGetProperty(section, out JsonElement sec) &&
                    sec.TryGetProperty(key, out JsonElement val) &&
                    val.ValueKind == JsonValueKind.String)
                {
                    return val.GetString() ?? @default;
                }
            }
            catch { /* ignore and fall back to default */ }
            return @default;
        }

        // ---------------------------------------------------------------------
        // Folder configuration
        // ---------------------------------------------------------------------
        string inputFolder = GetConfigString("Folders", "Input", @"C:\InputPdfs");
        string outputFolder = GetConfigString("Folders", "Output", @"C:\OutputPdfs");

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // ---------------------------------------------------------------------
        // Verify the input folder exists – if not, inform the user and exit.
        // ---------------------------------------------------------------------
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            Console.WriteLine("Create the folder and place PDF files inside, then re‑run the program.");
            return;
        }

        // ---------------------------------------------------------------------
        // Watermark configuration (read from the same JSON file)
        // ---------------------------------------------------------------------
        string watermarkText = GetConfigString("Watermark", "Text", "CONFIDENTIAL");
        string colorHex = GetConfigString("Watermark", "Color", "#FF0000"); // default Red
        Color watermarkColor = ColorTranslator.FromHtml(colorHex);
        string fontName = GetConfigString("Watermark", "FontName", "Arial");
        int fontSize = int.TryParse(GetConfigString("Watermark", "FontSize", "36"), out var fs) ? fs : 36;
        float opacity = float.TryParse(GetConfigString("Watermark", "Opacity", "0.5"), out var op) ? op : 0.5f;
        float originX = float.TryParse(GetConfigString("Watermark", "OriginX", "100"), out var ox) ? ox : 100f;
        float originY = float.TryParse(GetConfigString("Watermark", "OriginY", "400"), out var oy) ? oy : 400f;

        // ---------------------------------------------------------------------
        // Process each PDF file in the input folder
        // ---------------------------------------------------------------------
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.pdf");

            try
            {
                using (PdfFileStamp fileStamp = new PdfFileStamp())
                {
                    fileStamp.BindPdf(inputPath);

                    // Create a Stamp object – fully qualified to avoid ambiguity
                    Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

                    // Prepare formatted text for the watermark (FormattedText lives in Facades namespace)
                    FormattedText formattedText = new FormattedText(
                        watermarkText,
                        watermarkColor,
                        fontName,
                        EncodingType.Winansi,
                        false,          // do not embed the font
                        fontSize);

                    stamp.BindLogo(formattedText);
                    stamp.SetOrigin(originX, originY);
                    stamp.Opacity = opacity;
                    stamp.IsBackground = true; // place behind page content

                    fileStamp.AddStamp(stamp);
                    fileStamp.Save(outputPath);
                }

                Console.WriteLine($"Watermarked PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process '{inputPath}'. Error: {ex.Message}");
            }
        }
    }
}

/*
appsettings.json (place next to the compiled exe):
{
  "Folders": {
    "Input": "C:\\InputPdfs",
    "Output": "C:\\OutputPdfs"
  },
  "Watermark": {
    "Text": "CONFIDENTIAL",
    "Color": "#FF0000",   // HTML hex – red
    "FontName": "Arial",
    "FontSize": "36",
    "Opacity": "0.5",
    "OriginX": "100",
    "OriginY": "400"
  }
}
*/