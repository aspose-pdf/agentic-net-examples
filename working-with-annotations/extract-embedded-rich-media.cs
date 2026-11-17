using System;
using System.IO;
using System.Collections;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "ExtractedMedia";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root folder for extracted media exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    if (page.Annotations == null) continue;

                    int annotationCounter = 0;

                    // Iterate over all annotations on the page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Process only RichMediaAnnotations
                        if (annotation is RichMediaAnnotation richMedia)
                        {
                            annotationCounter++;

                            // Build a folder hierarchy: /ExtractedMedia/Page_{n}/Annotation_{m}_{Name}
                            string annotationFolder = Path.Combine(
                                outputRoot,
                                $"Page_{pageIndex}",
                                $"Annotation_{annotationCounter}_{richMedia.Name ?? "Unnamed"}");

                            Directory.CreateDirectory(annotationFolder);

                            // Use reflection to access RichMediaContent (may not exist in older versions)
                            PropertyInfo richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                            if (richMediaContentProp == null) continue; // Property not available – skip

                            object richMediaContent = richMediaContentProp.GetValue(richMedia);
                            if (richMediaContent == null) continue;

                            // Get the EmbeddedFiles collection via reflection
                            PropertyInfo embeddedFilesProp = richMediaContent.GetType().GetProperty("EmbeddedFiles");
                            if (embeddedFilesProp == null) continue;

                            IEnumerable embeddedFiles = embeddedFilesProp.GetValue(richMediaContent) as IEnumerable;
                            if (embeddedFiles == null) continue;

                            // Extract each embedded file
                            foreach (object embedded in embeddedFiles)
                            {
                                // File name
                                PropertyInfo nameProp = embedded.GetType().GetProperty("Name");
                                string fileName = nameProp?.GetValue(embedded) as string;
                                if (string.IsNullOrEmpty(fileName))
                                    fileName = $"Embedded_{Guid.NewGuid()}";

                                // File data – try "Data" (byte[]) first, then "DataStream" (Stream)
                                byte[] fileData = null;
                                PropertyInfo dataProp = embedded.GetType().GetProperty("Data");
                                if (dataProp != null)
                                    fileData = dataProp.GetValue(embedded) as byte[];

                                if (fileData == null)
                                {
                                    PropertyInfo streamProp = embedded.GetType().GetProperty("DataStream");
                                    Stream dataStream = streamProp?.GetValue(embedded) as Stream;
                                    if (dataStream != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            dataStream.CopyTo(ms);
                                            fileData = ms.ToArray();
                                        }
                                    }
                                }

                                if (fileData == null)
                                {
                                    Console.WriteLine($"Unable to extract data for embedded file '{fileName}'.");
                                    continue;
                                }

                                string filePath = Path.Combine(annotationFolder, fileName);
                                File.WriteAllBytes(filePath, fileData);
                                Console.WriteLine($"Saved: {filePath}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Media extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
