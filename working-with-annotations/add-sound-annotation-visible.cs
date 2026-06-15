using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace AsposePdfSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a simple PDF file to work with
            using (Document createDoc = new Document())
            {
                createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Ensure a small WAV file exists for the sound annotation
            string soundFilePath = "tone.wav";
            if (!File.Exists(soundFilePath))
            {
                // Minimal WAV header with a few bytes of silence (44‑byte header + data)
                byte[] wavData = new byte[]
                {
                    0x52, 0x49, 0x46, 0x46, 0x24, 0x08, 0x00, 0x00, // "RIFF" + size
                    0x57, 0x41, 0x56, 0x45,                         // "WAVE"
                    0x66, 0x6D, 0x74, 0x20,                         // "fmt "
                    0x10, 0x00, 0x00, 0x00,                         // Subchunk1Size (16)
                    0x01, 0x00,                                     // AudioFormat (PCM)
                    0x01, 0x00,                                     // NumChannels (1)
                    0x40, 0x1F, 0x00, 0x00,                         // SampleRate (8000)
                    0x80, 0x3E, 0x00, 0x00,                         // ByteRate (SampleRate*NumChannels*BitsPerSample/8)
                    0x02, 0x00,                                     // BlockAlign
                    0x10, 0x00,                                     // BitsPerSample (16)
                    0x64, 0x61, 0x74, 0x61,                         // "data"
                    0x00, 0x08, 0x00, 0x00,                         // Subchunk2Size (2048 bytes of silence placeholder)
                    // Silence data (few zero bytes – actual audio not required for demo)
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                };
                File.WriteAllBytes(soundFilePath, wavData);
            }

            // Load the PDF and add a SoundAnnotation
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100f, 600f, 200f, 650f);
                SoundAnnotation soundAnnotation = new SoundAnnotation(page, rect, soundFilePath);
                soundAnnotation.Icon = SoundIcon.Speaker;
                soundAnnotation.Contents = "Notification tone";
                // The annotation is visible by default; no Invisible/Hidden flags are set.
                page.Annotations.Add(soundAnnotation);

                doc.Save("output.pdf");
            }
        }
    }
}
