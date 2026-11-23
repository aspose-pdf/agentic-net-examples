using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedMedia";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            const int targetPageNumber = 16;
            if (targetPageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"PDF has only {pdfDoc.Pages.Count} pages. Page {targetPageNumber} does not exist.");
                return;
            }

            Page page = pdfDoc.Pages[targetPageNumber];

            int annotationIndex = 0;
            foreach (Annotation annot in page.Annotations)
            {
                // Filter only RichMediaAnnotation objects
                if (annot is RichMediaAnnotation richMedia)
                {
                    annotationIndex++;

                    // Use reflection to obtain RichMediaContent (may not exist in older versions)
                    PropertyInfo richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent", BindingFlags.Public | BindingFlags.Instance);
                    if (richMediaContentProp == null)
                    {
                        Console.WriteLine($"RichMediaAnnotation #{annotationIndex} does not expose RichMediaContent in this Aspose.Pdf version.");
                        continue;
                    }

                    object richMediaContent = richMediaContentProp.GetValue(richMedia);
                    if (richMediaContent == null)
                    {
                        Console.WriteLine($"RichMediaAnnotation #{annotationIndex} has no RichMediaContent instance.");
                        continue;
                    }

                    // EmbeddedFiles property on RichMediaContent
                    PropertyInfo embeddedFilesProp = richMediaContent.GetType().GetProperty("EmbeddedFiles", BindingFlags.Public | BindingFlags.Instance);
                    if (embeddedFilesProp == null)
                    {
                        Console.WriteLine($"RichMediaAnnotation #{annotationIndex} RichMediaContent does not expose EmbeddedFiles.");
                        continue;
                    }

                    var embeddedFiles = embeddedFilesProp.GetValue(richMediaContent) as System.Collections.IEnumerable;
                    if (embeddedFiles == null)
                    {
                        Console.WriteLine($"RichMediaAnnotation #{annotationIndex} contains no embedded files.");
                        continue;
                    }

                    int fileIndex = 0;
                    foreach (var embeddedFileObj in embeddedFiles)
                    {
                        fileIndex++;

                        // Retrieve Name and Data via reflection
                        Type efType = embeddedFileObj.GetType();
                        PropertyInfo nameProp = efType.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
                        PropertyInfo dataProp = efType.GetProperty("Data", BindingFlags.Public | BindingFlags.Instance);

                        string name = nameProp?.GetValue(embeddedFileObj) as string;
                        byte[] data = dataProp?.GetValue(embeddedFileObj) as byte[];

                        // Determine a safe file name
                        string baseName = !string.IsNullOrEmpty(name)
                            ? name
                            : $"annotation{annotationIndex}_file{fileIndex}";

                        // Replace invalid characters
                        string safeName = Path.GetInvalidFileNameChars()
                                            .Aggregate(baseName, (current, c) => current.Replace(c, '_'));

                        string outputPath = Path.Combine(outputFolder, safeName);

                        if (data != null && data.Length > 0)
                        {
                            File.WriteAllBytes(outputPath, data);
                            Console.WriteLine($"Saved embedded media to: {outputPath}");
                        }
                        else
                        {
                            Console.WriteLine($"Embedded file '{safeName}' has no data.");
                        }
                    }
                }
            }

            if (annotationIndex == 0)
            {
                Console.WriteLine($"No RichMediaAnnotation objects found on page {targetPageNumber}.");
            }
        }
    }
}
