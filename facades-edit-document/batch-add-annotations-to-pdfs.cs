using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;                                   // Core PDF API
using Aspose.Pdf.Facades;                          // Facade for annotation operations
using Aspose.Pdf.Annotations;                       // AnnotationType enum

// Model classes that match the expected JSON structure
public class AnnotationDefinition
{
    public string Type { get; set; }               // e.g., "Highlight", "Text"
    public string SourcePdf { get; set; }          // Path to PDF that already contains the annotation
}

public class PdfBatchJob
{
    public string InputPdf { get; set; }           // PDF to which annotations will be added
    public string OutputPdf { get; set; }          // Resulting PDF path
    public List<AnnotationDefinition> Annotations { get; set; }
}

class Program
{
    static void Main()
    {
        const string jsonPath = "jobs.json";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON definition file not found: {jsonPath}");
            return;
        }

        // Deserialize the JSON file into a list of batch jobs
        List<PdfBatchJob> jobs;
        try
        {
            string json = File.ReadAllText(jsonPath);
            jobs = JsonSerializer.Deserialize<List<PdfBatchJob>>(json, new JsonSerializerOptions
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

        // Process each job sequentially
        foreach (var job in jobs)
        {
            if (!File.Exists(job.InputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {job.InputPdf}");
                continue;
            }

            // Validate that each source PDF exists
            bool allSourcesExist = true;
            foreach (var ann in job.Annotations)
            {
                if (!File.Exists(ann.SourcePdf))
                {
                    Console.Error.WriteLine($"Source PDF for annotation not found: {ann.SourcePdf}");
                    allSourcesExist = false;
                }
            }
            if (!allSourcesExist) continue;

            try
            {
                // Open the target PDF inside a using block (ensures deterministic disposal)
                using (Document targetDoc = new Document(job.InputPdf))
                {
                    // Initialise the annotation editor with the opened document
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor(targetDoc))
                    {
                        // Prepare arrays required by ImportAnnotations overload
                        var sourcePaths = new List<string>();
                        var annotationTypes = new List<AnnotationType>();

                        foreach (var ann in job.Annotations)
                        {
                            sourcePaths.Add(ann.SourcePdf);

                            // Map string representation to the AnnotationType enum (case‑insensitive)
                            if (Enum.TryParse<AnnotationType>(ann.Type, true, out var at))
                            {
                                annotationTypes.Add(at);
                            }
                            else
                            {
                                Console.Error.WriteLine($"Unsupported annotation type: {ann.Type}");
                            }
                        }

                        // Perform the import – only if we have at least one valid type
                        if (sourcePaths.Count > 0 && annotationTypes.Count > 0)
                        {
                            editor.ImportAnnotations(sourcePaths.ToArray(),
                                                     annotationTypes.ToArray());
                        }

                        // Save the modified document to the desired output location
                        editor.Save(job.OutputPdf);
                    }
                }

                Console.WriteLine($"Processed '{job.InputPdf}' → '{job.OutputPdf}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{job.InputPdf}': {ex.Message}");
            }
        }
    }
}