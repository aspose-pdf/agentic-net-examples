using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class EmbedAudioFromXml
{
    static void Main()
    {
        // Input PDF and XML file paths
        const string pdfPath = "input.pdf";
        const string xmlPath = "audio.xml";
        const string outputPdfPath = "output_with_audio.pdf";

        // Verify files exist
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

        // Load the XML containing audio clip definitions
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block (ensures disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Expected XML format:
            // <AudioClips>
            //   <Clip page="1" x="100" y="500" width="20" height="20" file="audio1.wav" />
            //   ...
            // </AudioClips>
            foreach (XElement clipElem in xmlDoc.Root.Elements("Clip"))
            {
                // Parse attributes with defaults if missing
                int pageNumber = (int?)clipElem.Attribute("page") ?? 1;
                double x = (double?)clipElem.Attribute("x") ?? 0;
                double y = (double?)clipElem.Attribute("y") ?? 0;
                double width = (double?)clipElem.Attribute("width") ?? 20;
                double height = (double?)clipElem.Attribute("height") ?? 20;
                string soundFile = (string)clipElem.Attribute("file") ?? string.Empty;

                // Validate page number
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for clip '{soundFile}'. Skipping.");
                    continue;
                }

                // Resolve sound file path relative to the XML file location
                string soundFilePath = Path.IsPathRooted(soundFile)
                    ? soundFile
                    : Path.Combine(Path.GetDirectoryName(xmlPath) ?? "", soundFile);

                if (!File.Exists(soundFilePath))
                {
                    Console.Error.WriteLine($"Audio file not found: {soundFilePath}. Skipping.");
                    continue;
                }

                // Get the target page (1‑based indexing)
                Page targetPage = pdfDoc.Pages[pageNumber];

                // Define the annotation rectangle (lower‑left to upper‑right)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    x,                     // llx
                    y,                     // lly
                    x + width,             // urx
                    y + height);           // ury

                // Create the sound annotation and add it to the page
                SoundAnnotation soundAnn = new SoundAnnotation(targetPage, rect, soundFilePath);
                targetPage.Annotations.Add(soundAnn);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Audio annotations embedded. Output saved to '{outputPdfPath}'.");
    }
}