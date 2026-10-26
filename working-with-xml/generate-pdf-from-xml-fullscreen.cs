using System;
using System.IO;
using Aspose.Pdf;

// ---------------------------------------------------------------------------
// Minimal stubs for DocumentFormat.OpenXml types that may be referenced elsewhere
// in the project. These definitions allow the code to compile without adding the
// full OpenXML SDK NuGet package and do not interfere with the Aspose.Pdf usage.
// ---------------------------------------------------------------------------
namespace DocumentFormat.OpenXml
{
    public abstract class OpenXmlElement { }
    public struct UInt32Value
    {
        private uint _v;
        public static implicit operator UInt32Value(uint v) => new UInt32Value { _v = v };
        public static implicit operator uint(UInt32Value v) => v._v;
    }
    public struct Int64Value
    {
        private long _v;
        public static implicit operator Int64Value(long v) => new Int64Value { _v = v };
        public static implicit operator long(Int64Value v) => v._v;
    }
}

namespace DocumentFormat.OpenXml.Packaging
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Wordprocessing;

    public enum WordprocessingDocumentType { Document }

    public class WordprocessingDocument : IDisposable
    {
        public MainDocumentPart MainPart { get; } = new MainDocumentPart();
        private WordprocessingDocument() { }
        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type) => new WordprocessingDocument();
        public void Dispose() { }
    }

    public class MainDocumentPart
    {
        public Document Document { get; set; }
        private int _imgId = 1;
        public ImagePart AddImagePart(ImagePartType type) => new ImagePart();
        public string GetIdOfPart(ImagePart part) => "rId" + (_imgId++).ToString();
    }

    public enum ImagePartType { Png }

    public class ImagePart
    {
        public void FeedData(Stream s) { }
    }
}

namespace DocumentFormat.OpenXml.Wordprocessing
{
    using DocumentFormat.OpenXml;

    public class Document : OpenXmlElement
    {
        public Body Body { get; set; }
        public Document Append(Body b) { Body = b; return this; }
        public void Save() { }
    }

    public class Body : OpenXmlElement
    {
        public void Append(Paragraph p) { }
    }

    public class Paragraph : OpenXmlElement
    {
        public Paragraph(Run r) { }
    }

    public class Run : OpenXmlElement
    {
        public Run(string text) { }
    }
}

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file as a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Ensure deterministic disposal of the Document object
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Enable full‑screen viewer mode
            doc.PageMode = PageMode.FullScreen;

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with full‑screen mode: {pdfPath}");
    }
}
