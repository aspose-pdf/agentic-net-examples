using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "audioReferences.xml";
        const string outputPdf = "output_with_audio.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML that contains audio annotation definitions.
        // Expected format (example):
        // <Audios>
        //   <Audio page="1" llx="100" lly="500" urx="300" ury="550" file="sound1.wav" />
        //   <Audio page="2" llx="50"  lly="400" urx="250" ury="450" file="sound2.mp3" />
        // </Audios>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        using (Document pdfDoc = new Document(pdfPath))
        {
            foreach (XElement audioElem in xmlDoc.Root.Elements("Audio"))
            {
                // Parse required attributes safely.
                if (audioElem.Attribute("page") == null ||
                    audioElem.Attribute("llx") == null ||
                    audioElem.Attribute("lly") == null ||
                    audioElem.Attribute("urx") == null ||
                    audioElem.Attribute("ury") == null ||
                    audioElem.Attribute("file") == null)
                {
                    Console.Error.WriteLine("One or more required attributes are missing in an <Audio> element.");
                    continue;
                }

                int pageNumber = (int)audioElem.Attribute("page");
                double llx = (double)audioElem.Attribute("llx");
                double lly = (double)audioElem.Attribute("lly");
                double urx = (double)audioElem.Attribute("urx");
                double ury = (double)audioElem.Attribute("ury");
                string audioFile = (string)audioElem.Attribute("file");

                if (!File.Exists(audioFile))
                {
                    Console.Error.WriteLine($"Audio file not found: {audioFile}");
                    continue;
                }

                // Ensure the page exists (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for audio {audioFile}");
                    continue;
                }

                Page page = pdfDoc.Pages[pageNumber];

                // Create a rectangle that defines the annotation's location.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the sound annotation using the file path (string overload).
                SoundAnnotation soundAnno = new SoundAnnotation(page, rect, audioFile)
                {
                    Title    = Path.GetFileNameWithoutExtension(audioFile),
                    Contents = $"Audio: {Path.GetFileName(audioFile)}"
                };

                // Add the annotation to the page.
                page.Annotations.Add(soundAnno);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPdf}'.");
    }
}
