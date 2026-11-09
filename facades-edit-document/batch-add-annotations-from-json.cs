using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationBatch
{
    // Represents a single annotation definition from the JSON file
    public class AnnotationDefinition
    {
        public string Type { get; set; }          // "text", "link", "stamp"
        public int Page { get; set; }             // 1‑based page number
        public double Llx { get; set; }           // lower‑left X
        public double Lly { get; set; }           // lower‑left Y
        public double Urx { get; set; }           // upper‑right X
        public double Ury { get; set; }           // upper‑right Y
        public string Content { get; set; }       // text for "text", URL for "link"
        public string ImagePath { get; set; }     // path to image for "stamp"
    }

    // Represents a PDF processing job (input, output and its annotations)
    public class PdfJob
    {
        public required string InputPath { get; set; }
        public required string OutputPath { get; set; }
        // Initialise the collection to satisfy the non‑nullable warning
        public List<AnnotationDefinition> Annotations { get; set; } = new();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect the first argument to be the JSON definition file
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Usage: PdfAnnotationBatch <definition.json>");
                return;
            }

            string jsonPath = args[0];
            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"Definition file not found: {jsonPath}");
                return;
            }

            // Deserialize the JSON into a list of jobs
            List<PdfJob> jobs;
            try
            {
                string json = File.ReadAllText(jsonPath);
                jobs = JsonSerializer.Deserialize<List<PdfJob>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            if (jobs == null || jobs.Count == 0)
            {
                Console.WriteLine("No jobs defined in the JSON file.");
                return;
            }

            foreach (var job in jobs)
            {
                if (string.IsNullOrWhiteSpace(job.InputPath) || string.IsNullOrWhiteSpace(job.OutputPath))
                {
                    Console.Error.WriteLine("Job missing input or output path; skipping.");
                    continue;
                }

                if (!File.Exists(job.InputPath))
                {
                    Console.Error.WriteLine($"Input PDF not found: {job.InputPath}; skipping.");
                    continue;
                }

                // Ensure the output directory exists
                string outDir = Path.GetDirectoryName(job.OutputPath);
                if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
                {
                    Directory.CreateDirectory(outDir);
                }

                // Load the PDF document
                using Document doc = new Document(job.InputPath);

                // Process each annotation definition
                foreach (var ann in job.Annotations ?? new List<AnnotationDefinition>())
                {
                    // Build the rectangle (Aspose uses lower‑left X/Y, upper‑right X/Y)
                    var rect = new Aspose.Pdf.Rectangle(ann.Llx, ann.Lly, ann.Urx, ann.Ury);

                    // Validate page number (Aspose pages are 1‑based)
                    if (ann.Page < 1 || ann.Page > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {ann.Page} for {job.InputPath}; skipping annotation.");
                        continue;
                    }

                    Page page = doc.Pages[ann.Page];

                    switch (ann.Type?.Trim().ToLowerInvariant())
                    {
                        case "text":
                            var txtAnn = new TextAnnotation(page, rect)
                            {
                                Title = "Note",
                                Contents = ann.Content ?? string.Empty,
                                Open = true,
                                Icon = TextIcon.Note
                            };
                            page.Annotations.Add(txtAnn);
                            break;

                        case "link":
                            var linkAnn = new LinkAnnotation(page, rect)
                            {
                                Action = new GoToURIAction(ann.Content ?? string.Empty)
                            };
                            page.Annotations.Add(linkAnn);
                            break;

                        case "stamp":
                            if (!string.IsNullOrWhiteSpace(ann.ImagePath) && File.Exists(ann.ImagePath))
                            {
                                var stampAnn = new StampAnnotation(page, rect);
                                // Use a stream for the image as recommended by Aspose
                                byte[] imgBytes = File.ReadAllBytes(ann.ImagePath);
                                stampAnn.Image = new MemoryStream(imgBytes);
                                page.Annotations.Add(stampAnn);
                            }
                            else
                            {
                                Console.Error.WriteLine($"Stamp image not found: {ann.ImagePath}; skipping stamp annotation.");
                            }
                            break;

                        default:
                            Console.Error.WriteLine($"Unsupported annotation type '{ann.Type}' in job for {job.InputPath}; skipping.");
                            break;
                    }
                }

                // Save the modified PDF to the specified output path
                doc.Save(job.OutputPath);
                Console.WriteLine($"Processed '{job.InputPath}' → '{job.OutputPath}'.");
            }
        }
    }
}
