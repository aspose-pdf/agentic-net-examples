using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // source XML
        const string pdfPath = "output.pdf";  // result PDF
        const int lengthThresh = 1000;         // characters per page

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML document
        XDocument xDoc = XDocument.Load(xmlPath);

        // Disable Aspose.Pdf native helper executables (prevents missing‑exe runtime errors in restricted environments)
        // This must be set before any Aspose.Pdf API is used.
        Environment.SetEnvironmentVariable("Aspose.Pdf.DisableNative", "true");

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add the first page (Pages collection is 1‑based)
            pdfDoc.Pages.Add();

            int currentLength = 0; // accumulated text length on the current page

            // Iterate over XML elements that contain text (example: <Paragraph> nodes)
            foreach (XElement elem in xDoc.Descendants("Paragraph"))
            {
                string text = (elem.Value ?? string.Empty).Trim();
                if (string.IsNullOrEmpty(text))
                    continue;

                // If adding this text exceeds the threshold, start a new page
                if (currentLength + text.Length > lengthThresh)
                {
                    pdfDoc.Pages.Add();
                    currentLength = 0;
                }

                // Add the text fragment to the current (last) page
                TextFragment tf = new TextFragment(text);
                pdfDoc.Pages[pdfDoc.Pages.Count].Paragraphs.Add(tf);

                // Update accumulated length
                currentLength += text.Length;
            }

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated at '{pdfPath}'.");
    }
}
