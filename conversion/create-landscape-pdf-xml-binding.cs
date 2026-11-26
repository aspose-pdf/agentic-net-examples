using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "landscape.pdf";

        // XML that defines a single page with landscape orientation.
        // The <PageInfo IsLandscape='true'/> attribute forces the page to be landscape.
        string xml = @"<?xml version='1.0'?>
<Pages>
    <Page>
        <PageInfo IsLandscape='true' />
        <Paragraphs>
            <Paragraph>
                <TextFragment>Landscape page created via XML binding.</TextFragment>
            </Paragraph>
        </Paragraphs>
    </Page>
</Pages>";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Bind the XML definition to the document using a stream overload.
            // The string‑overload treats the argument as a file path, which causes a UriFormatException.
            using (var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                doc.BindXml(xmlStream);
            }

            // Save the document as a PDF file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
