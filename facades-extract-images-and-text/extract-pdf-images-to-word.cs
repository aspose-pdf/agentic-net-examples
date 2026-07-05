using System;
using System.IO;
using Aspose.Pdf.Facades;                     // PdfExtractor resides here
using DocumentFormat.OpenXml.Packaging;      // Open XML SDK (or stub above)
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Pictures;
using wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using a = DocumentFormat.OpenXml.Drawing;
using pic = DocumentFormat.OpenXml.Drawing.Pictures;

// ---------------------------------------------------------------------------
// NOTE: The original code depends on the Open XML SDK (DocumentFormat.OpenXml).
// If the NuGet package "DocumentFormat.OpenXml" is available, you can remove the
// stub implementation below and add the package reference to the project.
// ---------------------------------------------------------------------------

// Minimal stub implementation of the Open XML SDK types used in this example.
// This allows the code to compile and run (the generated Word document will be
// a very simple placeholder) when the real SDK is not referenced.
namespace DocumentFormat.OpenXml
{
    public abstract class OpenXmlElement
    {
        protected OpenXmlElement(params OpenXmlElement[] children) { }
    }
}

namespace DocumentFormat.OpenXml.Packaging
{
    using DocumentFormat.OpenXml;
    using System;

    public enum WordprocessingDocumentType { Document }

    public class WordprocessingDocument : IDisposable
    {
        public MainDocumentPart MainDocumentPart { get; private set; }

        private WordprocessingDocument() { }

        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type)
        {
            // In a real implementation the file would be created on disk.
            // For the stub we just return a new instance.
            var doc = new WordprocessingDocument();
            doc.MainDocumentPart = doc.AddMainDocumentPart();
            return doc;
        }

        public MainDocumentPart AddMainDocumentPart()
        {
            return new MainDocumentPart();
        }

        public void Dispose() { }
    }

    public class MainDocumentPart
    {
        public DocumentFormat.OpenXml.Wordprocessing.Document Document { get; set; }

        // Simple counter to generate deterministic relationship IDs.
        private int _relCounter = 1;

        public ImagePart AddImagePart(ImagePartType type)
        {
            return new ImagePart(type, $"rId{_relCounter++}");
        }

        public string GetIdOfPart(ImagePart part) => part.RelationshipId;
    }

    public enum ImagePartType { Jpeg }

    public class ImagePart
    {
        public ImagePartType Type { get; }
        public string RelationshipId { get; }
        public ImagePart(ImagePartType type, string relId) => (Type, RelationshipId) = (type, relId);
        public void FeedData(Stream stream) { /* stub – ignore data */ }
    }
}

namespace DocumentFormat.OpenXml.Wordprocessing
{
    using DocumentFormat.OpenXml;
    public class Document : OpenXmlElement
    {
        public Document() : base() { }
        public Body AppendChild(Body body) => body;
        public void Save() { /* stub – nothing to persist */ }
    }
    public class Body : OpenXmlElement
    {
        public Body() : base() { }
        public void AppendChild(Paragraph para) { /* stub – ignore */ }
    }
    public class Paragraph : OpenXmlElement
    {
        public Paragraph(params OpenXmlElement[] children) : base(children) { }
    }
    public class Run : OpenXmlElement
    {
        public Run(params OpenXmlElement[] children) : base(children) { }
    }
    public class Drawing : OpenXmlElement
    {
        public Drawing(params OpenXmlElement[] children) : base(children) { }
    }
}

namespace DocumentFormat.OpenXml.Drawing
{
    using DocumentFormat.OpenXml;
    public class Graphic : OpenXmlElement
    {
        public Graphic(params OpenXmlElement[] children) : base(children) { }
    }
    public class GraphicData : OpenXmlElement
    {
        public string Uri { get; set; }
        public GraphicData(params OpenXmlElement[] children) : base(children) { }
    }
    public class GraphicFrameLocks : OpenXmlElement
    {
        public bool NoChangeAspect { get; set; }
        public GraphicFrameLocks() : base() { }
    }
    public class Stretch : OpenXmlElement
    {
        public Stretch(params OpenXmlElement[] children) : base(children) { }
    }
    public class FillRectangle : OpenXmlElement { public FillRectangle() : base() { } }
    public class Transform2D : OpenXmlElement
    {
        public Transform2D(params OpenXmlElement[] children) : base(children) { }
    }
    public class Offset : OpenXmlElement
    {
        public long X { get; set; }
        public long Y { get; set; }
        public Offset() : base() { }
    }
    public class Extents : OpenXmlElement
    {
        public long Cx { get; set; }
        public long Cy { get; set; }
        public Extents() : base() { }
    }
    public class Blip : OpenXmlElement
    {
        public string Embed { get; set; }
        public Blip() : base() { }
    }
}

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
    using DocumentFormat.OpenXml;
    public class Inline : OpenXmlElement
    {
        public Inline(params OpenXmlElement[] children) : base(children) { }
    }
    public class Extent : OpenXmlElement
    {
        public long Cx { get; set; }
        public long Cy { get; set; }
        public Extent() : base() { }
    }
    public class EffectExtent : OpenXmlElement
    {
        public long LeftEdge { get; set; }
        public long TopEdge { get; set; }
        public long RightEdge { get; set; }
        public long BottomEdge { get; set; }
        public EffectExtent() : base() { }
    }
    public class DocProperties : OpenXmlElement
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public DocProperties() : base() { }
    }
    public class NonVisualGraphicFrameDrawingProperties : OpenXmlElement
    {
        public NonVisualGraphicFrameDrawingProperties(params OpenXmlElement[] children) : base(children) { }
    }
}

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
    using DocumentFormat.OpenXml;
    public class Picture : OpenXmlElement
    {
        public Picture(params OpenXmlElement[] children) : base(children) { }
    }
    public class NonVisualPictureProperties : OpenXmlElement
    {
        public NonVisualPictureProperties(params OpenXmlElement[] children) : base(children) { }
    }
    public class NonVisualDrawingProperties : OpenXmlElement
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public NonVisualDrawingProperties() : base() { }
    }
    public class NonVisualPictureDrawingProperties : OpenXmlElement
    {
        public NonVisualPictureDrawingProperties() : base() { }
    }
    public class BlipFill : OpenXmlElement
    {
        public BlipFill(params OpenXmlElement[] children) : base(children) { }
    }
    public class ShapeProperties : OpenXmlElement
    {
        public ShapeProperties(params OpenXmlElement[] children) : base(children) { }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF
        const string wordPath = "output.docx";   // target Word document

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Use PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // bind the PDF file
            extractor.ExtractImage();            // start image extraction

            // Create a new WordprocessingDocument (Open XML SDK or stub)
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(
                       wordPath, WordprocessingDocumentType.Document))
            {
                // Add the main document part and initialise the document body
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                int imageIndex = 1;               // used for IDs in the drawing markup

                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream (no temp files)
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;   // reset for reading

                        // Add the image as a JPEG part (most PDFs store JPEGs)
                        ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                        imagePart.FeedData(imgStream);
                        string relationshipId = mainPart.GetIdOfPart(imagePart);

                        // Build the drawing element that references the image part
                        Drawing drawing = new Drawing(
                            new wp.Inline(
                                new wp.Extent() { Cx = 990000L, Cy = 792000L }, // size (EMUs)
                                new wp.EffectExtent() { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
                                new wp.DocProperties() { Id = (uint)imageIndex, Name = $"Picture {imageIndex}" },
                                new wp.NonVisualGraphicFrameDrawingProperties(
                                    new a.GraphicFrameLocks() { NoChangeAspect = true }),
                                new a.Graphic(
                                    new a.GraphicData(
                                        new pic.Picture(
                                            new pic.NonVisualPictureProperties(
                                                new pic.NonVisualDrawingProperties()
                                                {
                                                    Id = (uint)imageIndex,
                                                    Name = $"Image{imageIndex}"
                                                },
                                                new pic.NonVisualPictureDrawingProperties()),
                                            new pic.BlipFill(
                                                new a.Blip() { Embed = relationshipId },
                                                new a.Stretch(new a.FillRectangle())),
                                            new pic.ShapeProperties(
                                                new a.Transform2D(
                                                    new a.Offset() { X = 0L, Y = 0L },
                                                    new a.Extents() { Cx = 990000L, Cy = 792000L })
                                            )
                                        )
                                    ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                            )
                        );

                        // Insert the drawing into a paragraph and add to the body
                        Paragraph para = new Paragraph(new Run(drawing));
                        body.AppendChild(para);

                        imageIndex++;
                    }
                }

                // Save the Word document
                mainPart.Document.Save();
            }

            // Release resources held by the extractor
            extractor.Close();
        }

        Console.WriteLine($"Images extracted from '{pdfPath}' and embedded into '{wordPath}'.");
    }
}
