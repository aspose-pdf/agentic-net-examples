using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "audio.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Load the XML that contains audio references
            XDocument xdoc = XDocument.Load(xmlPath);

            // Expected XML format:
            // <Audios>
            //   <Audio page="1" x="100" y="500" width="20" height="20" file="sound.wav" />
            //   ...
            // </Audios>

            foreach (var audioElem in xdoc.Descendants("Audio"))
            {
                // Read attributes with defaults
                int pageNum = (int?)audioElem.Attribute("page") ?? 1;
                double x = (double?)audioElem.Attribute("x") ?? 100;
                double y = (double?)audioElem.Attribute("y") ?? 500;
                double w = (double?)audioElem.Attribute("width") ?? 20;
                double h = (double?)audioElem.Attribute("height") ?? 20;
                string soundFile = (string)audioElem.Attribute("file");

                if (string.IsNullOrEmpty(soundFile) || !File.Exists(soundFile))
                {
                    Console.Error.WriteLine($"Audio file not found: {soundFile}");
                    continue;
                }

                // Validate page number
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number: {pageNum}");
                    continue;
                }

                Page page = doc.Pages[pageNum];

                // Define the annotation rectangle (lower‑left and upper‑right corners)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + w, y + h);

                // Create the sound annotation
                SoundAnnotation soundAnn = new SoundAnnotation(page, rect, soundFile);

                // Optional visual settings
                soundAnn.Contents = $"Audio: {Path.GetFileName(soundFile)}";

                // Add the annotation to the page
                page.Annotations.Add(soundAnn);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPath}'.");
    }
}