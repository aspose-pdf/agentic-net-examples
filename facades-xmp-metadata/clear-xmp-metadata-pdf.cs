using System;
using System.IO;
using Aspose.Pdf;

class XmpCleaner
{
    // Clears all XMP metadata fields, leaving only the required PDF schema header.
    public static void ClearXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Minimal XMP packet containing only the PDF schema header.
        const string minimalXmp = @"<?xpacket begin='' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
<rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
<rdf:Description rdf:about='' xmlns:pdf='http://ns.adobe.com/pdf/1.3/'>
</rdf:Description>
</rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

        // Load the PDF.
        Document pdfDocument = new Document(inputPdfPath);

        // ---------------------------------------------------------------------
        // NOTE: In recent Aspose.PDF versions the Document class no longer exposes
        // a "XmpMetadata" property.  XMP data is accessed through the
        // Document.Metadata dictionary (key/value pairs representing individual XMP
        // fields).  To remove all existing XMP entries we clear the dictionary and
        // then store the minimal packet as a custom entry.  This approach follows
        // the "use-document-metadata-indexer-instead-of-xmpmetadata" fix pattern.
        // ---------------------------------------------------------------------
        pdfDocument.Metadata.Clear();                 // remove every existing XMP field
        pdfDocument.Metadata["xmp"] = minimalXmp;    // store the minimal packet (key can be any identifier)

        // Save the cleaned PDF.
        pdfDocument.Save(outputPdfPath);
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        ClearXmpMetadata(inputPath, outputPath);
        Console.WriteLine($"XMP metadata cleared. Saved to '{outputPath}'.");
    }
}
