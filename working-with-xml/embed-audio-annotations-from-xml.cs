using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class EmbedAudioFromXml
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "audioReferences.xml";
        const string outputPath = "output_with_audio.pdf";

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

        // Load the XML that contains audio references.
        // Expected format:
        // <Audios>
        //   <Audio page="1" x="100" y="500" width="20" height="20" file="sound1.wav" />
        //   ...
        // </Audios>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Audio> element.
            foreach (XElement audioElem in xmlDoc.Root?.Elements("Audio") ?? Array.Empty<XElement>())
            {
                // Parse required attributes with fallback defaults.
                int pageNumber = (int?)audioElem.Attribute("page") ?? 1;
                double x = (double?)audioElem.Attribute("x") ?? 0;
                double y = (double?)audioElem.Attribute("y") ?? 0;
                double width = (double?)audioElem.Attribute("width") ?? 20;
                double height = (double?)audioElem.Attribute("height") ?? 20;
                string soundFile = (string)audioElem.Attribute("file");

                if (string.IsNullOrEmpty(soundFile) || !File.Exists(soundFile))
                {
                    Console.Error.WriteLine($"Audio file missing or not found: {soundFile}");
                    continue;
                }

                // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for audio {soundFile}");
                    continue;
                }

                // Create a rectangle that defines the annotation's location.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create the sound annotation. The constructor takes the page, rectangle, and the path to the sound file.
                SoundAnnotation soundAnn = new SoundAnnotation(pdfDoc.Pages[pageNumber], rect, soundFile)
                {
                    // Optional: set a tooltip and title. The Icon property is omitted because the enum may not be present in all SDK versions.
                    Title = Path.GetFileNameWithoutExtension(soundFile),
                    Contents = $"Play audio: {Path.GetFileName(soundFile)}"
                };

                // Add the annotation to the page.
                pdfDoc.Pages[pageNumber].Annotations.Add(soundAnn);
            }

            // Save the modified PDF. The using block ensures the document is properly disposed.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Audio annotations embedded and saved to '{outputPath}'.");
    }
}
