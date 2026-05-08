using System;
using System.IO;
using System.Xml.Linq;
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

        // Load the XML that contains audio annotation definitions.
        // Expected format (example):
        // <Audios>
        //   <Audio page="1" llx="100" lly="500" urx="200" ury="550" file="clip1.wav" />
        //   <Audio page="2" llx="150" lly="400" urx="250" ury="450" file="clip2.wav" />
        // </Audios>
        XDocument xDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Audio> element in the XML.
            foreach (XElement audioElem in xDoc.Root?.Elements("Audio") ?? Array.Empty<XElement>())
            {
                // Parse required attributes safely.
                if (!int.TryParse((string)audioElem.Attribute("page"), out int pageNumber) ||
                    !double.TryParse((string)audioElem.Attribute("llx"), out double llx) ||
                    !double.TryParse((string)audioElem.Attribute("lly"), out double lly) ||
                    !double.TryParse((string)audioElem.Attribute("urx"), out double urx) ||
                    !double.TryParse((string)audioElem.Attribute("ury"), out double ury) ||
                    string.IsNullOrWhiteSpace((string)audioElem.Attribute("file")))
                {
                    Console.Error.WriteLine("One or more required attributes are missing or invalid. Skipping this entry.");
                    continue;
                }

                string soundFile = (string)audioElem.Attribute("file");

                // Validate page index (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for audio file '{soundFile}'. Skipping.");
                    continue;
                }

                // Ensure the referenced sound file exists.
                if (!File.Exists(soundFile))
                {
                    Console.Error.WriteLine($"Audio file not found: {soundFile}. Skipping.");
                    continue;
                }

                // Create the rectangle that defines the annotation's location.
                // Aspose.Pdf.Rectangle expects double values.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Create the sound annotation on the specified page.
                // Constructor: SoundAnnotation(Page, Rectangle, string soundFile)
                SoundAnnotation soundAnn = new SoundAnnotation(pdfDoc.Pages[pageNumber], rect, soundFile)
                {
                    // Optional: set a title and contents for the annotation.
                    Title = Path.GetFileNameWithoutExtension(soundFile),
                    Contents = $"Play audio: {Path.GetFileName(soundFile)}"
                };

                // Add the annotation to the page's annotation collection.
                pdfDoc.Pages[pageNumber].Annotations.Add(soundAnn);
            }

            // Save the modified PDF. The using block ensures the document is disposed after saving.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Audio annotations embedded. Output saved to '{outputPdf}'.");
    }
}
