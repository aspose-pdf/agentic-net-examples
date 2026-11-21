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

        // Load the main PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all embedded file attachments
            foreach (FileSpecification attachment in doc.EmbeddedFiles)
            {
                // Process only PDF attachments
                if (attachment.MIMEType != null &&
                    attachment.MIMEType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Extract the attachment content into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        // "Contents" returns a Stream with the embedded file data
                        using (Stream src = attachment.Contents)
                        {
                            src.CopyTo(originalStream);
                        }
                        originalStream.Position = 0;

                        // Load the attached PDF
                        using (Document attachDoc = new Document(originalStream))
                        {
                            // Create a text stamp to serve as the watermark
                            TextStamp watermark = new TextStamp("CONFIDENTIAL")
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment   = VerticalAlignment.Center,
                                Opacity             = 0.3,
                                RotateAngle         = 45f // rotation in degrees
                            };
                            // TextState is read‑only; modify the existing instance
                            watermark.TextState.FontSize        = 72;
                            watermark.TextState.FontStyle       = FontStyles.Bold;
                            watermark.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // Red

                            // Apply the watermark to every page of the attachment
                            for (int i = 1; i <= attachDoc.Pages.Count; i++)
                            {
                                attachDoc.Pages[i].AddStamp(watermark);
                            }

                            // Save the modified attachment back to a memory stream
                            using (MemoryStream modifiedStream = new MemoryStream())
                            {
                                attachDoc.Save(modifiedStream);
                                modifiedStream.Position = 0;

                                // Replace the original attachment content with the watermarked version
                                attachment.Contents = modifiedStream;
                            }
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
