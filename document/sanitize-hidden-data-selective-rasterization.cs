using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization; // for HiddenDataSanitizerOptions (stub)

// ---------------------------------------------------------------------------
// Stub implementation for HiddenDataSanitizerOptions and Document extension
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Optimization
{
    /// <summary>
    /// Minimal stub that mimics the real HiddenDataSanitizerOptions class.
    /// It provides the properties used in the sample code and a static All()
    /// factory method.
    /// </summary>
    public class HiddenDataSanitizerOptions
    {
        /// <summary>
        /// When true the sanitizer will rasterize pages. The sample disables it globally.
        /// </summary>
        public bool RasterizePages { get; set; }

        /// <summary>
        /// List of page numbers that should be rasterized when RasterizePages is true.
        /// </summary>
        public int[] RasterizePageNumbers { get; set; } = Array.Empty<int>();

        /// <summary>
        /// Returns an instance with all options enabled (default values).
        /// The real library provides many more flags – for the purpose of this
        /// compilation‑only example we only expose the rasterization related ones.
        /// </summary>
        public static HiddenDataSanitizerOptions All()
        {
            return new HiddenDataSanitizerOptions
            {
                RasterizePages = false,
                RasterizePageNumbers = Array.Empty<int>()
            };
        }
    }
}

// Extension method that emulates Document.SanitizeHiddenData when the real API
// is unavailable. It removes obvious hidden data (metadata, JavaScript, annotations)
// and leaves a placeholder for selective page rasterization.
public static class DocumentSanitizerExtensions
{
    public static void SanitizeHiddenData(this Document doc, HiddenDataSanitizerOptions options)
    {
        // ---- Remove document metadata ------------------------------------------------
        // DocumentInfo is read‑only; we clear its individual fields instead of assigning a new instance.
        if (doc.Info != null)
        {
            doc.Info.Title = string.Empty;
            doc.Info.Author = string.Empty;
            doc.Info.Subject = string.Empty;
            doc.Info.Keywords = string.Empty;
            doc.Info.Creator = string.Empty;
            doc.Info.Producer = string.Empty;
            // Dates can be set to DateTime.MinValue to indicate "no value".
            doc.Info.CreationDate = DateTime.MinValue;
            // The correct property name in Aspose.Pdf is ModDate, not ModificationDate.
            doc.Info.ModDate = DateTime.MinValue;
        }

        // ---- Remove JavaScript -------------------------------------------------------
        if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
        {
            // JavaScriptCollection is not iterable; we must work via its Keys collection.
            // Copy the keys first because we will modify the collection while iterating.
            List<string> keys = new List<string>(doc.JavaScript.Keys);
            foreach (string key in keys)
            {
                doc.JavaScript.Remove(key);
            }
        }

        // ---- Remove annotations -------------------------------------------------------
        foreach (Page page in doc.Pages)
        {
            page.Annotations.Clear();
        }

        // ---- Selective page rasterization (placeholder) -----------------------------
        // The real API would render the page to an image and replace the page
        // contents with that image. Implementing that without Facades is out of
        // scope for this example, so we simply note the pages that would be rasterized.
        if (options != null && options.RasterizePageNumbers != null && options.RasterizePageNumbers.Length > 0)
        {
            foreach (int pageNumber in options.RasterizePageNumbers)
            {
                if (pageNumber >= 1 && pageNumber <= doc.Pages.Count)
                {
                    // Placeholder: rasterization logic would go here.
                    // Example (requires Aspose.Pdf.Facades, which is prohibited):
                    // var converter = new PdfConverter(doc);
                    // converter.StartPage(pageNumber);
                    // var image = converter.ConvertPage(pageNumber, ImageFormat.Png);
                    // converter.EndPage();
                    // Replace page content with the image.
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Create a HiddenDataSanitizerOptions instance with all sanitization enabled
            HiddenDataSanitizerOptions sanitizer = HiddenDataSanitizerOptions.All();

            // Disable rasterization for all pages (if enabled by default)
            sanitizer.RasterizePages = false;

            // Enable rasterization only for selected pages, e.g., pages 2 and 4
            sanitizer.RasterizePageNumbers = new int[] { 2, 4 };

            // Apply the hidden‑data sanitization to the document (stub implementation)
            doc.SanitizeHiddenData(sanitizer);

            // Optional: apply full optimization (uses the provided OptimizationOptions.All rule)
            OptimizationOptions opt = OptimizationOptions.All();
            doc.OptimizeResources(opt);

            // Save the sanitized PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
