using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the main PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the collection of embedded files (1‑based indexing)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            // Iterate backwards so we can safely replace items
            for (int idx = attachments.Count; idx >= 1; idx--)
            {
                FileSpecification spec = attachments[idx];
                string name = spec.Name; // use Name property (not FileName)

                // Process only PDF attachments (by file extension)
                if (!name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    continue;

                // Read the original attachment into a memory stream
                using (MemoryStream originalStream = new MemoryStream())
                {
                    spec.Contents.CopyTo(originalStream);
                    originalStream.Position = 0;

                    // Load the attached PDF
                    using (Document attachedDoc = new Document(originalStream))
                    {
                        // Prepare a text watermark stamp
                        TextStamp watermark = new TextStamp("CONFIDENTIAL");
                        // TextState is read‑only; modify its properties instead of assigning a new instance
                        watermark.TextState.FontSize = 72;
                        watermark.TextState.FontStyle = FontStyles.Bold;
                        watermark.TextState.ForegroundColor = Color.FromRgb(0.8, 0.8, 0.8);
                        watermark.Opacity = 0.3;
                        watermark.RotateAngle = 45;
                        watermark.HorizontalAlignment = HorizontalAlignment.Center;
                        watermark.VerticalAlignment = VerticalAlignment.Center;
                        watermark.Background = false;

                        // Apply the watermark to every page of the attached PDF
                        for (int i = 1; i <= attachedDoc.Pages.Count; i++)
                        {
                            attachedDoc.Pages[i].AddStamp(watermark);
                        }

                        // Save the modified attachment to a new memory stream
                        using (MemoryStream updatedStream = new MemoryStream())
                        {
                            attachedDoc.Save(updatedStream);
                            updatedStream.Position = 0;

                            // Remove the old attachment (by name, not by index)
                            attachments.Delete(name);

                            // Create a new FileSpecification from the updated stream
                            FileSpecification newSpec = new FileSpecification(updatedStream, name)
                            {
                                Description = spec.Description
                            };

                            // Add the new specification back to the collection
                            attachments.Add(newSpec);
                        }
                    }
                }
            }

            // Save the main document with updated attachments
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
