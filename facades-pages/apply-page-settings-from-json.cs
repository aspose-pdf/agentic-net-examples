using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PageConfig
{
    // Rotation in degrees (0, 90, 180, 270)
    public int? Rotation { get; set; }

    // Zoom factor (1.0 = 100%)
    public double? Zoom { get; set; }

    // Horizontal alignment: Left, Center, Right (not directly supported by Aspose, placeholder for future use)
    public string? HorizontalAlignment { get; set; }

    // Vertical alignment: Bottom, Middle, Top (not directly supported by Aspose, placeholder for future use)
    public string? VerticalAlignment { get; set; }

    // Desired page size (points). If both are set, PageSize will be applied.
    public double? PageWidth { get; set; }
    public double? PageHeight { get; set; }

    // List of page numbers to which the settings should be applied (1‑based indexing).
    public List<int>? ProcessPages { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - path to JSON configuration file
        // args[1] - input directory containing PDF files
        // args[2] - output directory for processed PDFs
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <exe> <config.json> <inputFolder> <outputFolder>");
            return;
        }

        string configPath = args[0];
        string inputFolder = args[1];
        string outputFolder = args[2];

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Deserialize JSON configuration
        PageConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<PageConfig>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the document
                Document pdfDocument = new Document(pdfPath);

                // Determine which pages to process
                IEnumerable<int> pagesToProcess;
                if (config.ProcessPages != null && config.ProcessPages.Count > 0)
                {
                    pagesToProcess = config.ProcessPages;
                }
                else
                {
                    pagesToProcess = Enumerable.Range(1, pdfDocument.Pages.Count);
                }

                // Apply rotation and custom page size directly on the Page objects
                foreach (int pageNumber in pagesToProcess)
                {
                    // Guard against out‑of‑range page numbers
                    if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
                        continue;

                    Page page = pdfDocument.Pages[pageNumber];

                    // Apply rotation if defined (cast int to Aspose.Pdf.Rotation enum)
                    if (config.Rotation.HasValue)
                    {
                        page.Rotate = (Rotation)config.Rotation.Value;
                    }

                    // Apply custom page size if dimensions are supplied
                    if (config.PageWidth.HasValue && config.PageHeight.HasValue)
                    {
                        page.PageInfo.Width = config.PageWidth.Value;
                        page.PageInfo.Height = config.PageHeight.Value;
                    }
                    else if (config.PageWidth.HasValue)
                    {
                        page.PageInfo.Width = config.PageWidth.Value;
                    }
                    else if (config.PageHeight.HasValue)
                    {
                        page.PageInfo.Height = config.PageHeight.Value;
                    }
                }

                // Apply zoom using PdfPageEditor if a zoom factor is defined
                if (config.Zoom.HasValue)
                {
                    var editor = new PdfPageEditor();
                    editor.BindPdf(pdfDocument);
                    editor.Zoom = (float)config.Zoom.Value;
                    editor.ProcessPages = pagesToProcess.ToArray();
                    editor.Save(outputPath);
                }
                else
                {
                    // Save the modified document without zoom processing
                    pdfDocument.Save(outputPath);
                }

                Console.WriteLine($"Processed '{fileName}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
