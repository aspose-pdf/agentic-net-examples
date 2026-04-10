using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "pdfa_compliant.pdf";
        const string logPath = "convert.log"; // log file for PDF/A conversion

        // Ensure we have a PDF to work with. If the file does not exist, create a simple one.
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            // Add a blank page so the document is not empty.
            doc.Pages.Add();
        }

        // Minimal XMP packet with required PDF/A entries:
        // - DocumentID (xmp:DocumentID)
        // - CreatorTool (xmp:CreatorTool)
        string xmpPacket = @"<?xpacket begin='' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about='' xmlns:xmp='http://ns.adobe.com/xap/1.0/'>
      <xmp:DocumentID>uuid:12345678-1234-1234-1234-1234567890ab</xmp:DocumentID>
      <xmp:CreatorTool>MyApp 1.0</xmp:CreatorTool>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

        // Keep the XMP stream alive for the whole conversion process.
        MemoryStream xmpStream = new MemoryStream(Encoding.UTF8.GetBytes(xmpPacket));
        try
        {
            // Assign XMP metadata to the document.
            doc.SetXmpMetadata(xmpStream);

            // Convert the document to PDF/A‑1b (or any PDF/A flavour you need).
            // The Convert method writes a log file; we keep it simple here.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑compliant file.
            doc.Save(outputPath);
        }
        finally
        {
            // Dispose the stream after the document has been saved.
            xmpStream.Dispose();
        }

        Console.WriteLine($"PDF/A‑compliant file saved to '{outputPath}'.");
    }
}
