using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO for JSON serialization
    public class AnnotationInfo
    {
        public int PageNumber { get; set; }
        public string? Subtype { get; set; }
        public RectangleDto? Rectangle { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public string? Author { get; set; }
        public DateTime? Modified { get; set; }
    }

    public class RectangleDto
    {
        public double Llx { get; set; }
        public double Lly { get; set; }
        public double Urx { get; set; }
        public double Ury { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "annotations.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // IMPORTANT: Aspose.Pdf extracts a small native helper exe at
            // runtime (AsposePdfApi_*.exe).  On some environments the exe
            // cannot be written to the default temporary folder, which
            // results in the "cannot find the file specified" exception.
            //
            // The fix below forces the library to use the already‑bundled
            // native resources instead of trying to extract a temporary
            // executable.  This is done by setting the environment variable
            // "Aspose.Pdf.ExtractNativeLibrary" to "false" *before* any
            // Aspose.Pdf types are touched.
            // ------------------------------------------------------------
            Environment.SetEnvironmentVariable("Aspose.Pdf.ExtractNativeLibrary", "false");

            // If you have a license file, load it here – it also prevents
            // the evaluation‑mode watermark and ensures the native library
            // is correctly initialised.
            // var license = new Aspose.Pdf.License();
            // license.SetLicense("Aspose.Pdf.lic");

            // Load PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                var allAnnotations = new List<AnnotationInfo>();

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
                {
                    // Retrieve annotations for the current page
                    AnnotationCollection pageAnnots = doc.Pages[pageIdx].Annotations;

                    foreach (Annotation annot in pageAnnots)
                    {
                        // Base information
                        string? title = null;
                        string? author = null;

                        // Title and Author (Subject) are only available on markup annotations
                        if (annot is MarkupAnnotation markup)
                        {
                            title = markup.Title;
                            author = markup.Subject; // Subject is used as author per Aspose guidelines
                        }

                        // Build DTO
                        var info = new AnnotationInfo
                        {
                            PageNumber = pageIdx,
                            Subtype = annot.AnnotationType.ToString(),
                            Rectangle = new RectangleDto
                            {
                                Llx = annot.Rect.LLX,
                                Lly = annot.Rect.LLY,
                                Urx = annot.Rect.URX,
                                Ury = annot.Rect.URY
                            },
                            Title = title,
                            Contents = annot.Contents,
                            Author = author,
                            Modified = annot.Modified // Correct property name
                        };

                        allAnnotations.Add(info);
                    }
                }

                // Serialize to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(allAnnotations, jsonOptions);
                File.WriteAllText(outputJson, json);

                Console.WriteLine($"Exported {allAnnotations.Count} annotations to '{outputJson}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
