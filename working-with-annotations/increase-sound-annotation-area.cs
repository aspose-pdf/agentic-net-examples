using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace IncreaseSoundAnnotationArea
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a minimal WAV file to be used for the sound annotation
            byte[] wavHeader = new byte[]
            {
                0x52, 0x49, 0x46, 0x46, 0x24, 0x08, 0x00, 0x00, // ChunkID & ChunkSize
                0x57, 0x41, 0x56, 0x45, 0x66, 0x6D, 0x74, 0x20, // Format & Subchunk1ID
                0x10, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, // Subchunk1Size, AudioFormat, NumChannels
                0x40, 0x1F, 0x00, 0x00, 0x80, 0x3E, 0x00, 0x00, // SampleRate, ByteRate
                0x02, 0x00, 0x10, 0x00, 0x64, 0x61, 0x74, 0x61, // BlockAlign, BitsPerSample, Subchunk2ID
                0x00, 0x08, 0x00, 0x00 // Subchunk2Size (silence)
            };
            File.WriteAllBytes("sample.wav", wavHeader);

            // Step 1: Create a sample PDF (self‑contained example)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Step 2: Open the PDF, add a SoundAnnotation and then enlarge its rectangle by 20%
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Add a sample SoundAnnotation on the first page
                Page firstPage = pdfDoc.Pages[1];
                Aspose.Pdf.Rectangle initialRect = new Aspose.Pdf.Rectangle(100f, 100f, 150f, 150f);
                SoundAnnotation soundAnn = new SoundAnnotation(firstPage, initialRect, "sample.wav");
                firstPage.Annotations.Add(soundAnn);

                // Iterate through all pages and annotations to adjust SoundAnnotations
                for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
                {
                    Page curPage = pdfDoc.Pages[pageIdx];
                    for (int annIdx = 1; annIdx <= curPage.Annotations.Count; annIdx++)
                    {
                        Annotation annotation = curPage.Annotations[annIdx];
                        SoundAnnotation soundAnnotation = annotation as SoundAnnotation;
                        if (soundAnnotation != null)
                        {
                            Aspose.Pdf.Rectangle oldRect = soundAnnotation.Rect;
                            float oldWidth = (float)(oldRect.URX - oldRect.LLX);
                            float oldHeight = (float)(oldRect.URY - oldRect.LLY);
                            float newWidth = oldWidth * 1.2f;
                            float newHeight = oldHeight * 1.2f;
                            float centerX = (float)((oldRect.LLX + oldRect.URX) / 2.0);
                            float centerY = (float)((oldRect.LLY + oldRect.URY) / 2.0);
                            float newLLX = centerX - newWidth / 2f;
                            float newLLY = centerY - newHeight / 2f;
                            float newURX = centerX + newWidth / 2f;
                            float newURY = centerY + newHeight / 2f;
                            Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(newLLX, newLLY, newURX, newURY);
                            soundAnnotation.Rect = newRect;
                        }
                    }
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}
