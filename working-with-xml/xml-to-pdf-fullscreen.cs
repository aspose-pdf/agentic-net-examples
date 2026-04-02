using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a simple XML string that defines PDF content
        string xmlContent = "<Document><Page><Paragraph>Sample text generated from XML.</Paragraph></Page></Document>";

        // Load the XML from a memory stream (avoids URI parsing issues with relative paths)
        using (var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
        using (Document doc = new Document())
        {
            // Bind the XML stream to the document
            doc.BindXml(xmlStream);

            // Set the viewer preference to open in full‑screen mode
            doc.PageMode = PageMode.FullScreen;
            // Optionally define how the document behaves after exiting full‑screen
            doc.NonFullScreenPageMode = PageMode.UseNone;

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}
