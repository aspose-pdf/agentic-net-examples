using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "audio_refs.xml"; // XML with audio references
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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(pdfPath))
        {
            // Load and parse the XML file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Expected XML format:
            // <Audios>
            //   <Audio page="1" x="100" y="500" width="20" height="20">audio1.wav</Audio>
            //   ...
            // </Audios>
            XmlNodeList? audioNodes = xmlDoc.SelectNodes("//Audio");
            if (audioNodes != null)
            {
                foreach (XmlNode node in audioNodes)
                {
                    // Guard against missing attributes
                    if (node.Attributes == null)
                    {
                        Console.Error.WriteLine("Audio node missing attributes. Skipping.");
                        continue;
                    }

                    // Extract attributes safely
                    if (!int.TryParse(node.Attributes["page"]?.Value, out int pageNumber) ||
                        !double.TryParse(node.Attributes["x"]?.Value, out double x) ||
                        !double.TryParse(node.Attributes["y"]?.Value, out double y) ||
                        !double.TryParse(node.Attributes["width"]?.Value, out double width) ||
                        !double.TryParse(node.Attributes["height"]?.Value, out double height))
                    {
                        Console.Error.WriteLine("Invalid attribute values for an Audio element. Skipping.");
                        continue;
                    }

                    string audioFile = node.InnerText.Trim();

                    // Validate page index (Aspose.Pdf uses 1‑based indexing)
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid page number {pageNumber} for audio '{audioFile}'. Skipping.");
                        continue;
                    }

                    // Verify that the audio file exists
                    if (!File.Exists(audioFile))
                    {
                        Console.Error.WriteLine($"Audio file not found: {audioFile}. Skipping.");
                        continue;
                    }

                    // Create a rectangle for the annotation location
                    // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

                    // Create the sound annotation on the specified page
                    // Constructor: SoundAnnotation(Page page, Rectangle rect, string soundFile)
                    SoundAnnotation soundAnn = new SoundAnnotation(doc.Pages[pageNumber], rect, audioFile)
                    {
                        // The Icon enum is not available in the current library version, so we omit it.
                        Title = Path.GetFileName(audioFile),
                        Contents = $"Audio: {Path.GetFileName(audioFile)}"
                    };

                    // Add the annotation to the page's annotation collection
                    doc.Pages[pageNumber].Annotations.Add(soundAnn);
                }
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded audio saved to '{outputPdf}'.");
    }
}
