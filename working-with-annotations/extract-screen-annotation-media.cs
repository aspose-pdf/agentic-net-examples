using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace ExtractMediaFromScreenAnnotation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a dummy media file to embed
            string mediaFilePath = "sample.mp4";
            byte[] dummyMedia = new byte[] { 0x00, 0x01, 0x02, 0x03 };
            File.WriteAllBytes(mediaFilePath, dummyMedia);

            // Step 2: Create a sample PDF with a ScreenAnnotation that embeds the media file
            using (Document doc = new Document())
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // Define annotation rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 600f);

                // Create the ScreenAnnotation – the constructor embeds the media file as an attachment
                ScreenAnnotation screenAnnotation = new ScreenAnnotation(page, rect, mediaFilePath);
                page.Annotations.Add(screenAnnotation);

                // Save the PDF
                doc.Save("sample.pdf");
            }

            // Step 3: Re‑open the PDF and extract the embedded media
            using (Document doc = new Document("sample.pdf"))
            {
                // The media file is stored as an embedded file in the document.
                // Retrieve the collection of embedded files.
                EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;
                if (embeddedFiles == null || embeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found.");
                    return;
                }

                // Look for the file named "sample.mp4"
                FileSpecification targetSpec = null;
                for (int i = 1; i <= embeddedFiles.Count; i++)
                {
                    FileSpecification spec = embeddedFiles[i];
                    if (spec != null && spec.Name != null && spec.Name.Equals("sample.mp4", StringComparison.OrdinalIgnoreCase))
                    {
                        targetSpec = spec;
                        break;
                    }
                }

                if (targetSpec == null)
                {
                    Console.WriteLine("Embedded media file not found.");
                    return;
                }

                // Extract the contents of the embedded file and write them to disk
                using (Stream sourceStream = targetSpec.Contents)
                {
                    using (FileStream fileStream = new FileStream("extracted_media.mp4", FileMode.Create, FileAccess.Write))
                    {
                        sourceStream.CopyTo(fileStream);
                    }
                }

                Console.WriteLine("Media file extracted successfully to 'extracted_media.mp4'.");
            }
        }
    }
}
