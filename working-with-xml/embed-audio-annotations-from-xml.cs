using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // source PDF
        const string xmlPath   = "audioReferences.xml"; // XML describing audio clips
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

        // Load the XML. Expected format:
        // <Audios>
        //   <Audio page="1" x="100" y="500" width="20" height="20">clip1.wav</Audio>
        //   <Audio page="2" x="150" y="400" width="20" height="20">clip2.mp3</Audio>
        // </Audios>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Audio> element.
            foreach (XElement audioElem in xmlDoc.Root.Elements("Audio"))
            {
                // Parse required attributes.
                int pageNumber = (int)audioElem.Attribute("page");
                double x        = (double)audioElem.Attribute("x");
                double y        = (double)audioElem.Attribute("y");
                double width    = (double)audioElem.Attribute("width");
                double height   = (double)audioElem.Attribute("height");
                string audioFilePath = audioElem.Value.Trim();

                // Validate page number (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for audio '{audioFilePath}'. Skipping.");
                    continue;
                }

                // Verify that the audio file exists.
                if (!File.Exists(audioFilePath))
                {
                    Console.Error.WriteLine($"Audio file not found: {audioFilePath}. Skipping.");
                    continue;
                }

                // Get the target page.
                Page page = pdfDoc.Pages[pageNumber];

                // Define the rectangle where the annotation will appear.
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                // Create the sound annotation using the constructor that accepts the audio file path.
                SoundAnnotation soundAnno = new SoundAnnotation(page, rect, audioFilePath)
                {
                    // Optional: give the annotation a name (appears in the PDF viewer UI).
                    Name = Path.GetFileNameWithoutExtension(audioFilePath),
                    // Optional: set an icon (Speaker is a common choice).
                    Icon = SoundIcon.Speaker
                };

                // Add the annotation to the page.
                page.Annotations.Add(soundAnno);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Audio annotations embedded. Output saved to '{outputPdf}'.");
    }
}
