using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a minimal WAV file (silence) to be used as audio
        string audioPath = "sample.wav";
        byte[] wavHeader = new byte[]
        {
            0x52, 0x49, 0x46, 0x46, 0x24, 0x08, 0x00, 0x00, 0x57, 0x41, 0x56, 0x45,
            0x66, 0x6D, 0x74, 0x20, 0x10, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00,
            0x40, 0x1F, 0x00, 0x00, 0x80, 0x3E, 0x00, 0x00, 0x02, 0x00, 0x10, 0x00,
            0x64, 0x61, 0x74, 0x61, 0x00, 0x08, 0x00, 0x00
        };
        File.WriteAllBytes(audioPath, wavHeader);

        // Step 1: Create a simple PDF file (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Step 2: Open the PDF, add a sound annotation and a JavaScript trigger
        using (Document pdfDoc = new Document("input.pdf"))
        {
            Page page = pdfDoc.Pages[1];

            // Add the sound annotation (audio embedded in the PDF)
            Aspose.Pdf.Rectangle soundRect = new Aspose.Pdf.Rectangle(100f, 500f, 200f, 600f);
            SoundAnnotation soundAnnot = new SoundAnnotation(page, soundRect, audioPath);
            page.Annotations.Add(soundAnnot);

            // Add a clickable area that runs JavaScript to play the sound annotation
            Aspose.Pdf.Rectangle jsRect = new Aspose.Pdf.Rectangle(100f, 500f, 200f, 600f);
            LinkAnnotation jsLink = new LinkAnnotation(page, jsRect);
            JavascriptAction jsAction = new JavascriptAction("this.getAnnots()[0].play();");
            jsLink.Action = jsAction;
            page.Annotations.Add(jsLink);

            pdfDoc.Save("output.pdf");
        }
    }
}