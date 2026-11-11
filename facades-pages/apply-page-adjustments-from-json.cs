using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

namespace PdfPageAutomation
{
    // Represents per‑page adjustments that can be applied.
    public class PageSettings
    {
        public int? Rotation { get; set; }               // Degrees: 0, 90, 180, 270
        public double? MarginLeft { get; set; }          // Points
        public double? MarginRight { get; set; }
        public double? MarginTop { get; set; }
        public double? MarginBottom { get; set; }
        public string? BackgroundColor { get; set; }      // Hex string, e.g. "#FFAA00"
    }

    // Represents a single PDF processing job.
    public class PdfJob
    {
        public required string InputPath { get; set; }   // Source PDF
        public required string OutputPath { get; set; }  // Destination PDF
        public required List<PageSettings> Pages { get; set; } // Settings per page (1‑based order)
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string configFile = "config.json";

            if (!File.Exists(configFile))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFile}");
                return;
            }

            List<PdfJob>? jobs;
            try
            {
                string json = File.ReadAllText(configFile);
                jobs = JsonSerializer.Deserialize<List<PdfJob>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse configuration: {ex.Message}");
                return;
            }

            if (jobs == null || jobs.Count == 0)
            {
                Console.WriteLine("No jobs defined in the configuration.");
                return;
            }

            foreach (var job in jobs)
            {
                if (!File.Exists(job.InputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {job.InputPath}");
                    continue;
                }

                // Load the PDF document.
                Document doc = new Document(job.InputPath);

                // Apply settings page by page. Aspose.Pdf uses 1‑based page indexing.
                for (int i = 0; i < job.Pages.Count; i++)
                {
                    int pageNumber = i + 1; // 1‑based index
                    PageSettings settings = job.Pages[i];

                    // Ensure the page exists.
                    if (pageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageNumber} does not exist in '{job.InputPath}'. Skipping.");
                        continue;
                    }

                    Page page = doc.Pages[pageNumber];

                    // Rotation
                    if (settings.Rotation.HasValue)
                    {
                        page.Rotate = GetRotationEnum(settings.Rotation.Value);
                    }

                    // Margins
                    if (settings.MarginLeft.HasValue || settings.MarginRight.HasValue ||
                        settings.MarginTop.HasValue || settings.MarginBottom.HasValue)
                    {
                        double left   = settings.MarginLeft   ?? page.PageInfo.Margin?.Left   ?? 0;
                        double right  = settings.MarginRight  ?? page.PageInfo.Margin?.Right  ?? 0;
                        double top    = settings.MarginTop    ?? page.PageInfo.Margin?.Top    ?? 0;
                        double bottom = settings.MarginBottom ?? page.PageInfo.Margin?.Bottom ?? 0;

                        page.PageInfo.Margin = new MarginInfo
                        {
                            Left = left,
                            Right = right,
                            Top = top,
                            Bottom = bottom
                        };
                    }

                    // Background color
                    if (!string.IsNullOrWhiteSpace(settings.BackgroundColor))
                    {
                        Aspose.Pdf.Color? bgColor = ParseHexColor(settings.BackgroundColor);
                        if (bgColor != null)
                        {
                            // Correct property: Page.Background (not PageInfo.BackgroundColor)
                            page.Background = bgColor;
                        }
                    }
                }

                // Save the modified PDF.
                doc.Save(job.OutputPath);
                Console.WriteLine($"Processed '{job.InputPath}' → '{job.OutputPath}'.");
            }
        }

        // Maps integer degrees to the Aspose.Pdf.Rotation enum.
        private static Rotation GetRotationEnum(int degrees)
        {
            return degrees switch
            {
                90  => Rotation.on90,
                180 => Rotation.on180,
                270 => Rotation.on270,
                _   => Rotation.None
            };
        }

        // Parses a hex color string (e.g. "#RRGGBB") into an Aspose.Pdf.Color.
        private static Aspose.Pdf.Color? ParseHexColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return null;

            hex = hex.TrimStart('#');
            if (hex.Length != 6)
                return null; // Invalid format.

            try
            {
                int r = Convert.ToInt32(hex.Substring(0, 2), 16);
                int g = Convert.ToInt32(hex.Substring(2, 2), 16);
                int b = Convert.ToInt32(hex.Substring(4, 2), 16);

                // Aspose.Pdf.Color expects values in the range 0‑1.
                return Aspose.Pdf.Color.FromRgb(r / 255.0, g / 255.0, b / 255.0);
            }
            catch
            {
                return null; // Parsing failed.
            }
        }
    }
}
