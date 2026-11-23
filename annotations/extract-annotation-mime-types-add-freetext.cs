using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_freetext.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect MIME types from existing annotations
            List<string> mimeTypes = new List<string>();

            foreach (Page page in doc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // ----- FileAttachmentAnnotation -----
                    if (annot is FileAttachmentAnnotation fileAnnot)
                    {
                        // Use reflection to stay compatible with all Aspose.Pdf versions
                        var fsProp = fileAnnot.GetType().GetProperty("FileSpecification");
                        if (fsProp != null)
                        {
                            var fileSpec = fsProp.GetValue(fileAnnot);
                            if (fileSpec != null)
                            {
                                var mimeProp = fileSpec.GetType().GetProperty("MimeType");
                                var mime = mimeProp?.GetValue(fileSpec) as string;
                                if (!string.IsNullOrEmpty(mime))
                                {
                                    mimeTypes.Add(mime);
                                    continue; // we have the MIME, go to next annotation
                                }

                                // Fallback – try to infer from the file name if MimeType is not exposed
                                var nameProp = fileSpec.GetType().GetProperty("FileName");
                                var name = nameProp?.GetValue(fileSpec) as string;
                                if (!string.IsNullOrEmpty(name))
                                {
                                    string ext = Path.GetExtension(name).ToLowerInvariant();
                                    var map = new Dictionary<string, string>
                                    {
                                        { ".pdf", "application/pdf" },
                                        { ".txt", "text/plain" },
                                        { ".jpg", "image/jpeg" },
                                        { ".jpeg", "image/jpeg" },
                                        { ".png", "image/png" },
                                        { ".doc", "application/msword" },
                                        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
                                    };
                                    if (map.TryGetValue(ext, out var inferred))
                                        mimeTypes.Add(inferred);
                                }
                            }
                        }
                    }
                    // ----- RichMediaAnnotation (optional) -----
                    else if (annot is RichMediaAnnotation richAnnot)
                    {
                        // RichMediaContent may not exist in older versions – use reflection safely
                        var rmcProp = richAnnot.GetType().GetProperty("RichMediaContent");
                        if (rmcProp != null)
                        {
                            var richMediaContent = rmcProp.GetValue(richAnnot);
                            if (richMediaContent != null)
                            {
                                // Try to get the first asset's MIME type (if the API provides it)
                                var assetsProp = richMediaContent.GetType().GetProperty("Assets");
                                var assets = assetsProp?.GetValue(richMediaContent) as System.Collections.IEnumerable;
                                if (assets != null)
                                {
                                    foreach (var asset in assets)
                                    {
                                        var mimeProp = asset.GetType().GetProperty("MimeType");
                                        var mime = mimeProp?.GetValue(asset) as string;
                                        if (!string.IsNullOrEmpty(mime))
                                        {
                                            mimeTypes.Add(mime);
                                            break; // we only need one MIME per annotation
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Prepare the text to display in the FreeText annotation
            string freeTextContent = mimeTypes.Count == 0
                ? "No annotation MIME types found."
                : "Annotation MIME Types:\n- " + string.Join("\n- ", mimeTypes);

            // Create a FreeText annotation on the first page
            Page firstPage = doc.Pages[1];
            // Rectangle for the annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 650, 550, 750);

            // FreeTextAnnotation requires a DefaultAppearance (font, size, color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            FreeTextAnnotation freeText = new FreeTextAnnotation(firstPage, rect, appearance)
            {
                Contents = freeTextContent,
                Color = Aspose.Pdf.Color.LightYellow, // background colour of the annotation
                Justification = Justification.Left
            };

            // Border (required for visible border) – no Color property on Border
            freeText.Border = new Border(freeText) { Width = 1 };

            // Add the annotation to the page
            firstPage.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FreeText annotation: {outputPath}");
    }
}
