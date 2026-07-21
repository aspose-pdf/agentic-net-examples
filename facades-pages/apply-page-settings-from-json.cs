using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Represents the page‑specific settings that can be defined in the JSON file.
    private class PageSettings
    {
        public int? Rotation { get; set; }                     // 0, 90, 180, 270
        public float? Zoom { get; set; }                       // 1.0 = 100%
        public string? HorizontalAlignment { get; set; }        // "Left", "Center", "Right"
        public string? VerticalAlignment { get; set; }          // "Top", "Center", "Bottom"
        public int? DisplayDuration { get; set; }              // seconds
        public int? TransitionDuration { get; set; }           // seconds
        public string? TransitionType { get; set; }             // e.g., "BLINDH", "DISSOLVE"
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "pageSettings.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load the JSON configuration into a dictionary keyed by page number (1‑based).
        Dictionary<int, PageSettings> config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<Dictionary<int, PageSettings>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Process the PDF.
        try
        {
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a PdfPageEditor bound to the document.
                PdfPageEditor editor = new PdfPageEditor(doc);

                // Iterate over each page that has a configuration entry.
                foreach (var kvp in config)
                {
                    int pageNumber = kvp.Key;
                    PageSettings settings = kvp.Value;

                    // Ensure the page number is within the document range.
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Page {pageNumber} is out of range. Skipping.");
                        continue;
                    }

                    // Apply settings to a single page by restricting ProcessPages.
                    editor.ProcessPages = new int[] { pageNumber };

                    if (settings.Rotation.HasValue)
                        editor.Rotation = settings.Rotation.Value; // 0,90,180,270

                    if (settings.Zoom.HasValue)
                        editor.Zoom = settings.Zoom.Value; // float, 1.0 = 100%

                    if (!string.IsNullOrWhiteSpace(settings.HorizontalAlignment))
                    {
                        // Map string to Aspose.Pdf.HorizontalAlignment enum.
                        editor.HorizontalAlignment = settings.HorizontalAlignment.Trim().ToLower() switch
                        {
                            "left"   => Aspose.Pdf.HorizontalAlignment.Left,
                            "center" => Aspose.Pdf.HorizontalAlignment.Center,
                            "right"  => Aspose.Pdf.HorizontalAlignment.Right,
                            _ => editor.HorizontalAlignment // keep existing if unknown
                        };
                    }

                    if (!string.IsNullOrWhiteSpace(settings.VerticalAlignment))
                    {
                        // Map string to Aspose.Pdf.VerticalAlignment enum.
                        editor.VerticalAlignmentType = settings.VerticalAlignment.Trim().ToLower() switch
                        {
                            "top"    => Aspose.Pdf.VerticalAlignment.Top,
                            "center" => Aspose.Pdf.VerticalAlignment.Center,
                            "bottom" => Aspose.Pdf.VerticalAlignment.Bottom,
                            _ => editor.VerticalAlignmentType // keep existing if unknown
                        };
                    }

                    if (settings.DisplayDuration.HasValue)
                        editor.DisplayDuration = settings.DisplayDuration.Value; // int seconds

                    if (settings.TransitionDuration.HasValue)
                        editor.TransitionDuration = settings.TransitionDuration.Value; // int seconds

                    if (!string.IsNullOrWhiteSpace(settings.TransitionType))
                    {
                        // Map string to the corresponding transition constant.
                        editor.TransitionType = settings.TransitionType.Trim().ToUpper() switch
                        {
                            "BLINDH" => PdfPageEditor.BLINDH,
                            "BLINDV" => PdfPageEditor.BLINDV,
                            "BTWIPE" => PdfPageEditor.BTWIPE,
                            "DGLITTER" => PdfPageEditor.DGLITTER,
                            "DISSOLVE" => PdfPageEditor.DISSOLVE,
                            "INBOX" => PdfPageEditor.INBOX,
                            "LRGLITTER" => PdfPageEditor.LRGLITTER,
                            "LRWIPE" => PdfPageEditor.LRWIPE,
                            "OUTBOX" => PdfPageEditor.OUTBOX,
                            "RLWIPE" => PdfPageEditor.RLWIPE,
                            "SPLITHIN" => PdfPageEditor.SPLITHIN,
                            "SPLITHOUT" => PdfPageEditor.SPLITHOUT,
                            "SPLITVIN" => PdfPageEditor.SPLITVIN,
                            "SPLITVOUT" => PdfPageEditor.SPLITVOUT,
                            "TBGLITTER" => PdfPageEditor.TBGLITTER,
                            "TBWIPE" => PdfPageEditor.TBWIPE,
                            _ => editor.TransitionType // keep existing if unknown
                        };
                    }

                    // Apply the accumulated changes to the current page.
                    editor.ApplyChanges();
                }

                // Save the modified document.
                doc.Save(outputPdfPath);
                Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
