using System;
using System.IO;
using Aspose.Pdf.Facades;               // PdfExtractor
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging; // WordprocessingDocument
using DocumentFormat.OpenXml.Wordprocessing; // Document, Body, Paragraph, Run
using DocumentFormat.OpenXml.Drawing;   // Drawing elements
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Pictures;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF
        const string docxPath  = "output.docx"; // target Word document

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create a new WordprocessingDocument (DOCX) inside a using block for deterministic disposal
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(docxPath, WordprocessingDocumentType.Document))
        {
            // Add the main document part and initialise an empty body
            MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body());

            // Initialise the PDF extractor facade
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);          // bind the source PDF
                extractor.ExtractImage();            // start image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0; // reset for reading

                        // Add the image as an ImagePart to the Word document
                        // Assume JPEG; if needed, detect format and use the appropriate ImagePartType
                        ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                        imagePart.FeedData(imgStream);

                        // Insert the picture into the document body
                        AddPictureToBody(mainPart.GetIdOfPart(imagePart), mainPart.Document.Body, imageIndex);
                    }

                    imageIndex++;
                }

                extractor.Close(); // release resources held by the facade
            }

            // Save the Word document
            mainPart.Document.Save();
        }

        Console.WriteLine($"Images extracted from '{pdfPath}' and embedded into '{docxPath}'.");
    }

    // Helper method to create a picture drawing and append it to the document body
    private static void AddPictureToBody(string relationshipId, Body body, int imageNumber)
    {
        // Define the drawing element that references the image part
        Drawing element = new Drawing(
                new Inline(
                    new Extent() { Cx = 990000L, Cy = 792000L }, // size in EMUs (adjust as needed)
                    new EffectExtent()
                    {
                        LeftEdge = 0L,
                        TopEdge = 0L,
                        RightEdge = 0L,
                        BottomEdge = 0L
                    },
                    new DocProperties()
                    {
                        Id = (UInt32Value)(uint)imageNumber,
                        Name = $"Picture {imageNumber}"
                    },
                    new NonVisualGraphicFrameDrawingProperties(
                        new GraphicFrameLocks() { NoChangeAspect = true }),
                    new Graphic(
                        new GraphicData(
                            new Picture(
                                new NonVisualPictureProperties(
                                    new NonVisualDrawingProperties()
                                    {
                                        Id = (UInt32Value)(uint)imageNumber,
                                        Name = $"Image{imageNumber}.jpg"
                                    },
                                    new NonVisualPictureDrawingProperties()),
                                new BlipFill(
                                    new Blip() { Embed = relationshipId },
                                    new Stretch(new FillRectangle())),
                                new ShapeProperties(
                                    new Transform2D(
                                        new Offset() { X = 0L, Y = 0L },
                                        new Extents() { Cx = 990000L, Cy = 792000L })
                                )
                            )
                        ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                )
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U,
                    EditId = "50D07946"
                });

        // Wrap the drawing in a Run -> Paragraph and add to the body
        Paragraph paragraph = new Paragraph(new Run(element));
        body.Append(paragraph);
    }
}

// ---------------------------------------------------------------------------
// Minimal stubs for the Open XML SDK types used in this example.
// In a real project you should reference the official DocumentFormat.OpenXml
// NuGet package (version 2.15 or later). The stubs below allow the code to
// compile in environments where the package is not available.
// ---------------------------------------------------------------------------
namespace DocumentFormat.OpenXml
{
    public abstract class OpenXmlElement { }
    public class UInt32Value : OpenXmlElement
    {
        private uint _value;
        public static implicit operator UInt32Value(uint v) => new UInt32Value { _value = v };
        public static implicit operator uint(UInt32Value v) => v._value;
    }
}

namespace DocumentFormat.OpenXml.Packaging
{
    public enum WordprocessingDocumentType { Document }
    public class WordprocessingDocument : IDisposable
    {
        private readonly string _path;
        private readonly WordprocessingDocumentType _type;
        private MainDocumentPart _mainPart;
        private WordprocessingDocument(string path, WordprocessingDocumentType type)
        {
            _path = path; _type = type;
        }
        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type) => new WordprocessingDocument(path, type);
        public MainDocumentPart AddMainDocumentPart()
        {
            _mainPart = new MainDocumentPart();
            return _mainPart;
        }
        public void Dispose() { /* In a real implementation the file would be written here */ }
    }

    public class MainDocumentPart
    {
        public Document Document { get; set; }
        public ImagePart AddImagePart(ImagePartType type) => new ImagePart();
        public string GetIdOfPart(ImagePart part) => "rId" + Guid.NewGuid().ToString("N");
    }

    public enum ImagePartType { Jpeg }
    public class ImagePart
    {
        public void FeedData(Stream stream) { /* store the stream data */ }
    }
}

namespace DocumentFormat.OpenXml.Wordprocessing
{
    using DocumentFormat.OpenXml;
    public class Document : OpenXmlElement
    {
        public Body Body { get; }
        public Document(Body body) { Body = body; }
        public void Save() { /* In a real implementation the document XML would be written */ }
    }
    public class Body : OpenXmlElement
    {
        public void Append(Paragraph p) { /* add paragraph to body */ }
    }
    public class Paragraph : OpenXmlElement
    {
        public Paragraph(params OpenXmlElement[] children) { }
    }
    public class Run : OpenXmlElement
    {
        public Run(params OpenXmlElement[] children) { }
    }
}

namespace DocumentFormat.OpenXml.Drawing
{
    using DocumentFormat.OpenXml;
    public class Drawing : OpenXmlElement
    {
        public Drawing(params OpenXmlElement[] children) { }
    }
    public class Inline : OpenXmlElement
    {
        public Inline(Extent extent, EffectExtent effectExtent, DocProperties docProperties,
                      NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties,
                      Graphic graphic)
        { }
        public UInt32Value DistanceFromTop { get; set; }
        public UInt32Value DistanceFromBottom { get; set; }
        public UInt32Value DistanceFromLeft { get; set; }
        public UInt32Value DistanceFromRight { get; set; }
        public string EditId { get; set; }
    }
    public class Extent : OpenXmlElement
    {
        public long Cx { get; set; }
        public long Cy { get; set; }
    }
    public class EffectExtent : OpenXmlElement
    {
        public long LeftEdge { get; set; }
        public long TopEdge { get; set; }
        public long RightEdge { get; set; }
        public long BottomEdge { get; set; }
    }
    public class DocProperties : OpenXmlElement
    {
        public UInt32Value Id { get; set; }
        public string Name { get; set; }
    }
    public class NonVisualGraphicFrameDrawingProperties : OpenXmlElement
    {
        public NonVisualGraphicFrameDrawingProperties(GraphicFrameLocks locks) { }
    }
    public class GraphicFrameLocks : OpenXmlElement
    {
        public bool NoChangeAspect { get; set; }
    }
    public class Graphic : OpenXmlElement
    {
        public Graphic(GraphicData data) { }
    }
    public class GraphicData : OpenXmlElement
    {
        public GraphicData(Picture picture) { }
        public string Uri { get; set; }
    }
}

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
    using DocumentFormat.OpenXml;
    public class Picture : OpenXmlElement
    {
        public Picture(NonVisualPictureProperties nonVisualPictureProperties, BlipFill blipFill, ShapeProperties shapeProperties) { }
    }
    public class NonVisualPictureProperties : OpenXmlElement
    {
        public NonVisualPictureProperties(NonVisualDrawingProperties nonVisualDrawingProperties, NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties) { }
    }
    public class NonVisualDrawingProperties : OpenXmlElement
    {
        public UInt32Value Id { get; set; }
        public string Name { get; set; }
    }
    public class NonVisualPictureDrawingProperties : OpenXmlElement { }
    public class BlipFill : OpenXmlElement
    {
        public BlipFill(Blip blip, Stretch stretch) { }
    }
    public class Blip : OpenXmlElement
    {
        public string Embed { get; set; }
    }
    public class Stretch : OpenXmlElement
    {
        public Stretch(FillRectangle fillRect) { }
    }
    public class FillRectangle : OpenXmlElement { }
    public class ShapeProperties : OpenXmlElement
    {
        public ShapeProperties(Transform2D transform) { }
    }
    public class Transform2D : OpenXmlElement
    {
        public Transform2D(Offset offset, Extents extents) { }
    }
    public class Offset : OpenXmlElement
    {
        public long X { get; set; }
        public long Y { get; set; }
    }
    public class Extents : OpenXmlElement
    {
        public long Cx { get; set; }
        public long Cy { get; set; }
    }
}

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
    // No additional types needed for this example.
}
